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
using System.Threading;

namespace CodeOwls.PowerShell.Host.Executors
{
    public class AsyncCommandResult : IAsyncResult
    {
        private readonly AsyncCallback _callback;
        private readonly string _command;
        private readonly ExecutionOptions _executionOptions;
        private readonly Dictionary<string, object> _parameters;
        private readonly object _state;
        private long _complete;
        private ManualResetEvent _completeEvent;
        private Exception _exception;
        private Collection<PSObject> _results;
        private bool _synchronous;

        public AsyncCommandResult(string command, Dictionary<string, object> parameters,
                                  ExecutionOptions executionOptions, AsyncCallback callback, object state)
        {
            _command = command;
            _parameters = parameters;
            _executionOptions = executionOptions;
            _callback = callback;
            _state = state;
        }

        public Dictionary<string, object> Parameters
        {
            get { return _parameters; }
        }

        public string Command
        {
            get { return _command; }
        }

        public ExecutionOptions ExecutionOptions
        {
            get { return _executionOptions; }
        }

        #region IAsyncResult Members

        public bool IsCompleted
        {
            get { return 0 != Interlocked.Read(ref _complete); }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (null == _completeEvent)
                {
                    var ev = new ManualResetEvent(false);
                    var existing = Interlocked.CompareExchange(ref _completeEvent, ev, null);
                    if (null != existing)
                    {
                        ev.Close();
                    }
                }

                return _completeEvent;
            }
        }

        public object AsyncState
        {
            get { return _state; }
        }

        public bool CompletedSynchronously
        {
            get { return _synchronous; }
        }

        #endregion

        internal Collection<PSObject> GetCommandResults()
        {
            if (null != _exception)
            {
                throw _exception;
            }
            return _results;
        }

        internal void SetComplete(Collection<PSObject> results, bool synchronous, Exception e)
        {
            long alreadySet = Interlocked.CompareExchange(ref _complete, 1, 0);
            if (0 != alreadySet)
            {
                throw new InvalidOperationException("this async command result has already been completed");
            }

            _results = results;
            _synchronous = synchronous;
            _exception = e;

            var ev = Interlocked.Exchange(ref _completeEvent, null);
            if (null != ev)
            {
                ev.Set();
                ev.Close();
            }

            if (null != _callback)
            {
                _callback(this);
            }
        }
    }
}
