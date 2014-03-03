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
using System.Management.Automation;
using CodeOwls.PowerShell.Host.Utility;

namespace CodeOwls.PowerShell.Host.Executors
{
    public abstract class AsyncCommandExecutorBase : ICommandExecutor
    {
        private readonly SyncQueue<AsyncCommandResult> _queue;

        protected AsyncCommandExecutorBase()
        {
            _queue = new SyncQueue<AsyncCommandResult>();
        }

        protected AsyncCommandExecutorBase(SyncQueue<AsyncCommandResult> queue)
        {
            _queue = queue;
        }

        protected internal SyncQueue<AsyncCommandResult> Queue
        {
            get { return _queue; }
        }

        #region ICommandExecutor Members

        public Collection<PSObject> Execute(string command)
        {
            return Execute(command, null);
        }

        public Collection<PSObject> Execute(string command, Dictionary<string, object> parameters)
        {
            var ar = BeginExecute(command, parameters, false, null, null);
            return EndExecute(ar);
        }

        public IAsyncResult BeginExecute(string command, Dictionary<string, object> parameters, ExecutionOptions options,
                                         AsyncCallback callback, object asyncState)
        {
            AsyncCommandResult asyncCommandResult = new AsyncCommandResult(command, parameters, options, callback,
                                                                           asyncState);
            Queue.Enqueue(asyncCommandResult);

            return asyncCommandResult;
        }

        public Collection<PSObject> EndExecute(IAsyncResult ar)
        {
            AsyncCommandResult asyncCommandResult = ar as AsyncCommandResult;
            if (null == asyncCommandResult)
            {
                throw new InvalidOperationException("the IAsyncResult provided is not of the appropriate type");
            }

            while (!asyncCommandResult.IsCompleted)
            {
                asyncCommandResult.AsyncWaitHandle.WaitOne(100);
                DoWait();
            }
            return asyncCommandResult.GetCommandResults();
        }

        public abstract bool CancelCurrentExecution(int timeoutInMilliseconds);
        public abstract CommandExecutorState CurrentState { get; }

        #endregion

        public IAsyncResult BeginExecute(string command, Dictionary<string, object> parameters, bool outputToConsole,
                                         AsyncCallback callback, object asyncState)
        {
            ExecutionOptions options = ExecutionOptions.DoNotRaisePipelineException | (
                outputToConsole ? ExecutionOptions.AddOutputter : ExecutionOptions.None );
            return BeginExecute(command, parameters, options, callback, asyncState);
        }

        protected internal void DoWait()
        {
        }
    }
}
