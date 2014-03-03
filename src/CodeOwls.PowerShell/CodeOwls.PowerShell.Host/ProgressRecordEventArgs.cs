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
using System.Management.Automation;

namespace CodeOwls.PowerShell.Host
{
    public class ProgressRecordEventArgs : EventArgs
    {
        private readonly ProgressRecord _record;
        private readonly long _sourceId;


        public ProgressRecordEventArgs(long sourceId, ProgressRecord record)
        {
            _sourceId = sourceId;
            _record = record;
        }

        public ProgressRecord ProgressRecord
        {
            get { return _record; }
        }

        public long SourceId
        {
            get { return _sourceId; }
        }
    }
}
