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
    internal class CommandAutoCompleteProvider : IAutoCompleteProvider
    {
        private readonly string _commandTemplate;
        private readonly Executor _executor;

        protected CommandAutoCompleteProvider(string commandTemplate, Executor executor)
        {
            _commandTemplate = commandTemplate;
            _executor = executor;
        }

        #region IAutoCompleteProvider Members

        public virtual IEnumerable<string> GetSuggestions(string guess)
        {
            if( String.IsNullOrEmpty(guess))
            {
                return new string[0];
            }

            var info = FormatGuessInfo(guess);
            IEnumerable<ErrorRecord> error;
            var items = _executor.ExecuteCommand(GetCommand(info), out error, ExecutionOptions.DoNotRaisePipelineException);
            if (null == items)
            {
                return new string[] {};
            }

            return items.ToList()
                .ConvertAll(d => d.ToStringValue())
                .ConvertAll(d => d.Contains(" ") ? String.Format("'{0}'", d) : d)
                .ConvertAll(d => String.Format(info.CommandFormatString, d));
        }

        #endregion

        protected virtual string GetCommand(FormattedGuessInformation info)
        {
            return String.Format(_commandTemplate, info.Guess);
        }

        protected string[] BreakIntoWords(string guess)
        {
            Collection<PSParseError> errors;
            var tokens = PSParser.Tokenize( guess, out errors );
            var parts = from token in tokens
                        select token.Content;

            if (parts.Any())
            {
                return parts.ToArray();
            }

            guess = guess.Trim();
            Regex re = new Regex(@"('[^']+(?:'|$))|(""[^""]+(?:""|$))|([^\s'""]+)");
            var matches = re.Matches(guess);
            return (from Match match in matches
                    let value =
                        String.IsNullOrEmpty(match.Groups[1].Value) ? match.Groups[0].Value : match.Groups[1].Value
                    select value).ToArray();

            //return Regex.Split(guess, @"\s+");
            /*char[] quotes = new char[] {'\'', '"'};
            
            if( (-1) != guess.IndexOfAny( quotes ))
            {
                List<string> quotedParts = new List<string>();
                Regex reQuotes = new Regex( @"['""]" );
                parts.ToList().ForEach(
                    part =>
                        {
                            
                        }
                );
            }
            return parts;*/
        }

        protected virtual FormattedGuessInformation FormatGuessInfo(string guess)
        {
            var words = BreakIntoWords(guess);
            var guessTemplate = words.LastOrDefault();
            var re = new Regex(Regex.Escape(guessTemplate) + ".*?$");
            var commandFormat = re.Replace( guess, "{0}");
            guessTemplate = guessTemplate.TrimStart('\'', '"').TrimEnd('\'', '"');
            if (! guessTemplate.Contains("*"))
            {
                guessTemplate += "*";
            }

            return new FormattedGuessInformation(guessTemplate, commandFormat);
        }

        #region Nested type: FormattedGuessInformation

        public class FormattedGuessInformation
        {
            public FormattedGuessInformation(string guess, string commandFormat)
            {
                Guess = guess;
                CommandFormatString = commandFormat;
            }

            public string Guess { get; private set; }
            public string CommandFormatString { get; private set; }
        }

        #endregion
    }
}
