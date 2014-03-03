/*
   Copyright (c) 2011 Code Owls LLC, All Rights Reserved.

   Licensed under the Microsoft Reciprocal License (Ms-RL) (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.opensource.org/licenses/ms-rl

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;
using CodeOwls.PowerShell.Host.Utility;

namespace CodeOwls.PowerShell.Host.Executors
{
    internal class Executor
    {
        private readonly Runspace _runspace;
        private Pipeline _currentPipeline;

        public Executor(Runspace runspace)
        {
            _runspace = runspace;
        }

        private bool IsRunspaceBusy
        {
            get
            {
                return _runspace.RunspaceAvailability != RunspaceAvailability.Available;
            }
        }

        public event EventHandler<EventArgs<ErrorRecord>> PipelineError;

        public bool CancelCurrentPipeline(int timeoutInMilliseconds, Action pipelineCancelFailCallback )
        {
            var pipeline = _currentPipeline;
            var activeStates = new[] {PipelineState.Running, PipelineState.NotStarted, PipelineState.Stopping};
            var inactiveStates = new[] {PipelineState.Stopped, PipelineState.Failed, PipelineState.Completed};
            if (null != pipeline &&
                activeStates.Contains(pipeline.PipelineStateInfo.State))
            {
                Timer cancelTimer = null;
                TimerCallback callback = o =>
                                             {
                                                 try
                                                 {
                                                     pipelineCancelFailCallback();
                                                     cancelTimer.Change(-1, -1);
                                                     cancelTimer.Dispose();
                                                     cancelTimer = null;
                                                 }
                                                 catch
                                                 {

                                                 }
                                             };

                cancelTimer = new Timer( callback, null, 
                    TimeSpan.FromMilliseconds( timeoutInMilliseconds), TimeSpan.FromMilliseconds(-1));

                pipeline.StateChanged += (s, a) =>
                                             {
                                                 if( null != cancelTimer && 
                                                     inactiveStates.Contains(a.PipelineStateInfo.State) )
                                                 {
                                                     cancelTimer.Change(-1, -1);
                                                     cancelTimer.Dispose();
                                                     cancelTimer = null;
                                                 }
                                             };
                pipeline.StopAsync();                
            }

            return true;
        }

        public string ExecuteAndGetStringResult(string command, out IEnumerable<ErrorRecord> exceptionThrown)
        {
            var results = ExecuteCommand(command, out exceptionThrown, ExecutionOptions.None);

            if (null == results || ! results.Any())
            {
                return null;
            }

            PSObject pso = results[0];
            if (null == pso)
            {
                return String.Empty;
            }
            if (null == pso.BaseObject)
            {
                return pso.ToString();
            }
            return pso.BaseObject.ToString();
        }

        public Collection<PSObject> OutputObjects(IEnumerable<object> inputs,
                                                   out IEnumerable<ErrorRecord> error, ExecutionOptions options)
        {
            var pipe = _runspace.CreatePipeline();
            
            if (null != inputs && inputs.Any())
            {
                pipe.Input.Write(inputs, true);
            }

            return ExecuteCommandHelper(pipe, out error, options);
        }

        public Collection<PSObject> ExecuteCommand(string command, Dictionary<string, object> inputs,
                                                   out IEnumerable<ErrorRecord> error, ExecutionOptions options)
        {
            var isscript = null == inputs || !inputs.Any();
            var cmd = new Command(command, isscript, false);

            if (null != inputs && inputs.Any())
            {
                inputs.ToList().ForEach(pair =>
                    cmd.Parameters.Add(pair.Key, pair.Value));
            }

            var pipe = _runspace.CreatePipeline();

            pipe.Commands.Add(cmd);

            return ExecuteCommandHelper(pipe, out error, options);
        }

        public Collection<PSObject> ExecuteCommand(string command, out IEnumerable<ErrorRecord> error, ExecutionOptions options)
        {
            var pipe = _runspace.CreatePipeline(command,
                                                ExecutionOptions.None != (ExecutionOptions.AddToHistory & options));
            return ExecuteCommandHelper(pipe, out error, options);
        }

        private void RaisePipelineExceptionEvent(ErrorRecord e)
        {
            EventHandler<EventArgs<ErrorRecord>> handler = PipelineError;
            if (handler != null)
            {
                handler(this, new EventArgs<ErrorRecord>(e));
            }
        }

        private Collection<PSObject> ExecuteCommandHelper(Pipeline tempPipeline, out IEnumerable<ErrorRecord> exceptionThrown,
                                                          ExecutionOptions options)
        {
            exceptionThrown = null;
            Collection<PSObject> collection = null;
            ApplyExecutionOptionsToPipeline(options, tempPipeline);
            collection = ExecutePipeline(options, tempPipeline, collection, out exceptionThrown);
            return collection;
        }

        private static void ApplyExecutionOptionsToPipeline(ExecutionOptions options, Pipeline tempPipeline)
        {
            if ((options & ExecutionOptions.AddOutputter) != ExecutionOptions.None)
            {
                if (tempPipeline.Commands.Count == 1)
                {
                    tempPipeline.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);
                }
                //var item = new Command("Out-Default", false, true);
                var item = new Command("Out-Default");
                tempPipeline.Commands.Add(item);
            }
        }

        private Collection<PSObject> ExecutePipeline(ExecutionOptions options, Pipeline tempPipeline,
                                                     Collection<PSObject> collection, out IEnumerable<ErrorRecord> exceptionThrown)
        {
            exceptionThrown = new List<ErrorRecord>();
            try
            {
                bool acquired = Monitor.TryEnter(_runspace);
                if (! acquired)
                {
                    return null;
                }

                try
                {
                    _currentPipeline = tempPipeline;

                    List<ErrorRecord> exception = new List<ErrorRecord>();
                    try
                    {
                        WaitWhileRunspaceIsBusy();

                        tempPipeline.StateChanged += OnPipelineStateChange;
                        try
                        {
                            ExecutePipeline(options, tempPipeline);
                        }
                        catch (PSInvalidOperationException ioe)
                        {
                            /*
                             * HACK: there seems to be some lag between the toggle of the runspace
                             * availability state and the clearing of the runspace's current
                             * pipeline state.  it's possible for the runspace to report it's available
                             * for use and then raise a PSInvalidOperationException indicating that another
                             * pipeline is currently executing.
                             * 
                             * This is a hacky way around the issue - wait 1/3 of a second for the 
                             * runspace state to clear and try again.
                             * 
                             * I've also tried adding a WaitWhilePipelineIsRunning method that spins
                             * on a DoWait while the pipeline is not in the completed or failed state;
                             * however this seems to slow the execution down considerably.  
                             */
                            if (tempPipeline.PipelineStateInfo.State == PipelineState.NotStarted)
                            {
                                Thread.Sleep(333);
                                ExecutePipeline(options, tempPipeline);
                            }
                        }
                        tempPipeline.Input.Close();

                        // WaitWhilePipelineIsRunning(tempPipeline);                    

                        collection = tempPipeline.Output.ReadToEnd();
                        if (null != tempPipeline.PipelineStateInfo.Reason)
                        {
                            throw tempPipeline.PipelineStateInfo.Reason;
                        }
                        exception.AddRange( GetPipelineErrors(options, tempPipeline) );
                    }
                    catch( RuntimeException re )
                    {
                        exception.Add(re.ErrorRecord);
                    }
                    catch( Exception e )
                    {
                        exception.Add(new ErrorRecord( e, e.GetType().FullName, ErrorCategory.NotSpecified, null));
                    }
                    finally
                    {
                        _currentPipeline = null;
                    }

                    if (exception.Any() )
                    {
                        
                        if (!options.HasFlag(ExecutionOptions.DoNotRaisePipelineException))
                        {
                            exception.ToList().ForEach( RaisePipelineExceptionEvent );
                        }
                        exceptionThrown = exception;
                    }
                }
                finally
                {
                    Monitor.Exit(_runspace);
                }
            }
            catch (Exception ex)
            {
                exceptionThrown = new ErrorRecord[]
                                        {
                                            new ErrorRecord( ex, ex.GetType().FullName, ErrorCategory.NotSpecified, null), 
                                        };
            }
            return collection;
        }

        private static void ExecutePipeline(ExecutionOptions options, Pipeline tempPipeline)
        {
            //if (options.HasFlag(ExecutionOptions.Synchronous))
            //{
            //    tempPipeline.Invoke();
            //}
            //else
            {
                tempPipeline.InvokeAsync();
            }
        }

        public readonly ManualResetEvent RunspaceReady = new ManualResetEvent(false);
        private void OnPipelineStateChange(object sender, PipelineStateEventArgs e)
        {
            switch( e.PipelineStateInfo.State )
            {
                case( PipelineState.Completed ):
                case( PipelineState.Failed) :
                case( PipelineState.Stopped ):
                    {
                        ((Pipeline)sender).StateChanged -= OnPipelineStateChange;

                        RunspaceReady.Set();
                        break;
                    }
                case( PipelineState.Running):
                    {
                        RunspaceReady.Reset();
                        break;

                    }
                default:
                    break;
            }
        }

        private void WaitWhilePipelineIsRunning(Pipeline tempPipeline)
        {
            while( tempPipeline.PipelineStateInfo.State != PipelineState.Completed &&
                tempPipeline.PipelineStateInfo.State != PipelineState.Failed )
            {
                DoWait();
            }
        }

        private void WaitWhileRunspaceIsBusy()
        {
            while (IsRunspaceBusy)
            {
                DoWait();
            }
        }

        private void DoWait()
        {

        }

        private List<ErrorRecord> GetPipelineErrors(ExecutionOptions options, Pipeline tempPipeline)
        {
            List<ErrorRecord> pipelineErrors = new List<ErrorRecord>();

            if ( 0 < tempPipeline.Error.Count)
            {
                var error = tempPipeline.Error.Read();
                var errorRecord = error.ToPSObject().BaseObject as ErrorRecord;
                if (null == errorRecord)
                {
                    errorRecord = new ErrorRecord( error  as Exception, "", ErrorCategory.CloseError, null);
                }

                pipelineErrors.Add(errorRecord);                
            }
            
            return pipelineErrors;
        }
    }
}
