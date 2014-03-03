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
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CodeOwls.PowerShell.Host.Utility
{
    public class SyncQueue<T>
    {
        private readonly Queue<T> _q;
        private readonly ManualResetEvent _qv;

        public SyncQueue()
        {
            _q = new Queue<T>();
            _qv = new ManualResetEvent(false);
        }

        public WaitHandle WaitHandle
        {
            get { return _qv; }
        }

        public void Enqueue(T item)
        {
            lock (_q)
            {
                _q.Enqueue(item);
            }
            _qv.Set();
        }

        public T Dequeue()
        {
            _qv.WaitOne();
            T t = default(T);

            lock (_q)
            {
                t = _q.Dequeue();

                if (0 == _q.Count)
                {
                    _qv.Reset();
                }
            }
            return t;
        }

        public List<T> DequeueAny()
        {
            lock (_q)
            {
                var l = _q.ToList();
                _q.Clear();
                return l;
            }
        }
    }
}
