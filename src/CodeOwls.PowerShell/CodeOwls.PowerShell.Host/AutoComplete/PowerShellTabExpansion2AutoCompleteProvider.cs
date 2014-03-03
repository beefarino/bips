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
using CodeOwls.PowerShell.Host.Executors;
using CodeOwls.PowerShell.Host.Utility;

namespace CodeOwls.PowerShell.Host.AutoComplete
{
    internal class PowerShellTabExpansion2AutoCompleteProvider : IAutoCompleteProvider
    {
        private bool? _enabled; 
        private readonly Executor _executor;
        private const string TabExpansionScript = @"TabExpansion2 -input '{0}' -cur {1} | foreach {{
        $i = $_.replacementindex;
        $_.completionmatches | foreach {{
            '{0}'.substring(0,$i) + $_.completiontext;
        }}
    }}";

        private const string TabExpansionFunctionName = "TabExpansion2";
        private const string InputParameterName = "InputScript";
        private const string CursorPositionParameterName = "CursorColumn";
        
        public PowerShellTabExpansion2AutoCompleteProvider( Executor executor )
        {
            _executor = executor;
        }

        public IEnumerable<string> GetSuggestions(string guess)
        {
            if (! _enabled.HasValue)
            {
                InitializeEnabled();
            }

            if (! _enabled.Value )
            {
                return new string[] {};
            }

            guess = ( guess ?? String.Empty ).Replace( "'", "`'");
            
            try
            {
                var script = String.Format(TabExpansionScript, guess, guess.Length);
                IEnumerable<ErrorRecord> error;
                var results = _executor.ExecuteCommand(script, out error,
                                                       ExecutionOptions.None);
                if (null == results)
                {
                    return new string[] { };
                }
               
                return results.ToList().ConvertAll(pso => pso.ToStringValue());
            }
            catch
            {
            }
            return null;
            
        }

        private void InitializeEnabled()
        {
            IEnumerable<ErrorRecord> error;
            bool enabled;
            var result = _executor.ExecuteAndGetStringResult("test-path function:/tabexpansion2", out error);
            if (null != error)
            {
                _enabled = false;
                return;
            }
            
            bool.TryParse(result, out enabled);
            _enabled = enabled;
        }
    }
}
