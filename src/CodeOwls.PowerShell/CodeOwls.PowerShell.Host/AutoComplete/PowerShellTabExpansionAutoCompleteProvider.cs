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
using System.Linq;
using System.Management.Automation;
using System.Text.RegularExpressions;
using CodeOwls.PowerShell.Host.Executors;
using CodeOwls.PowerShell.Host.Utility;

namespace CodeOwls.PowerShell.Host.AutoComplete
{
    internal class PowerShellTabExpansionAutoCompleteProvider : IAutoCompleteProvider
    {
        private const string TabExpansionFunctionName = "TabExpansion";
        private const string LineArgumentName = "line";
        private const string LastWordArgumentName = "lastWord";
        private readonly Executor _executor;

        public PowerShellTabExpansionAutoCompleteProvider(Executor executor)
        {
            _executor = executor;
        }

        #region IAutoCompleteProvider Members

        public IEnumerable<string> GetSuggestions(string guess)
        {
            try
            {
                IEnumerable<ErrorRecord> error;
                Collection<PSParseError> errors;
                
                IEnumerable<PSToken> tokens = PSParser.Tokenize( guess, out errors );
                
                if (null == tokens || ! tokens.Any())
                {
                    return new string[] {};
                }

                var lastToken = tokens.Last();
                var lastWord = lastToken.Content;
                
                if (PSTokenType.Variable == lastToken.Type)
                {
                    lastWord = "$" + lastWord;
                }
                
                var arguments = new Dictionary<string, object>
                                    {
                                        {LineArgumentName, guess},
                                        {LastWordArgumentName, lastWord}
                                    };

                var results = _executor.ExecuteCommand(TabExpansionFunctionName, arguments, out error,
                                                       ExecutionOptions.None);
                if (null == results)
                {
                    return new string[] {};
                }

                //var regex = new Regex(Regex.Escape(lastWord) + @"$");

                return results.ToList()
                    .ConvertAll(pso => pso.ToStringValue())
                    .ConvertAll(s=> guess.Remove( lastToken.Start ) + s);
            }
            catch
            {
            }
            return null;
        }

        #endregion

        private Dictionary<string, object> SplitGuessIntoArguments(string guess)
        {
            Collection<PSParseError> errors;
            var tokens = System.Management.Automation.PSParser.Tokenize(guess, out errors);

            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add(LineArgumentName, guess);
            
            //todo: add more logic to split, handle quotations
            var lastWord = Regex.Split(guess, @"\s+").LastOrDefault();
            args.Add(LastWordArgumentName, lastWord);
            return args;
        }
    }
}
