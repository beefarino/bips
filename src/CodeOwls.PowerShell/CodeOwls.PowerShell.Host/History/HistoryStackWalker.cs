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
using System.Linq;
using System.Management.Automation;
using System.Threading;
using CodeOwls.PowerShell.Host.Executors;

namespace CodeOwls.PowerShell.Host.History
{
    internal class HistoryStackWalker : IHistoryStackWalker
    {
        private readonly Executor _executor;
        private HistoryStack _history;
        private int _index;

        public HistoryStackWalker(Executor executor)
        {
            _executor = executor;
            _index = 0;
        }

        #region IHistoryStackWalker Members

        public string Newest()
        {
            bool isReset = LoadHistory();

            if (!_history.Any())
            {
                return null;
            }

            _index = _history.Count - 1;

            return _history[_index];
        }

        public string Oldest()
        {
            bool isReset = LoadHistory();

            if (!_history.Any())
            {
                return null;
            }

            _index = 0;

            return _history[_index];
        }

        public void Reset()
        {
            _history = null;
            _index = 0;
        }

        public string NextUp()
        {
            bool isReset = LoadHistory();

            if (!_history.Any())
            {
                return null;
            }

            _index -= 1;
            _index = Math.Min(_history.Count - 1, Math.Max(0, _index));

            return _history[_index];
        }

        public string NextDown()
        {
            bool isReset = LoadHistory();

            if (!_history.Any())
            {
                return null;
            }

            _index += 1;
            _index = Math.Max(0, Math.Min(_history.Count - 1, _index));

            return _history[_index];
        }

        #endregion

        private bool LoadHistory()
        {
            if (null != _history)
            {
                return false;
            }

            _history = new HistoryStack();
            IEnumerable<ErrorRecord> error;
            var items = _executor.ExecuteCommand("get-history", out error, ExecutionOptions.DoNotRaisePipelineException);
            if (null == items)
            {
                return false;
            }

            items.ToList().ForEach(
                item =>
                    {
                        var o = item.BaseObject ?? item;
                        if (null == o)
                        {
                            return;
                        }
                        _history.Add(o.ToString());
                    });
            _index = _history.Count;

            return true;
        }
    }
}
