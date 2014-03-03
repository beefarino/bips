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
using System.Linq;
using System.Text.RegularExpressions;
using CodeOwls.PowerShell.Host.Executors;

namespace CodeOwls.PowerShell.Host.AutoComplete
{
    internal class DriveListAutoCompleteProvider : CommandAutoCompleteProvider
    {
        private const string Command =
            @"get-psdrive |  ?{{ $_.Name.ToLowerInvariant().StartsWith( '{0}' ) }} | %{{ $_.name + ':' }}";

        public DriveListAutoCompleteProvider(Executor executor)
            : base(Command, executor)
        {
        }

        protected override FormattedGuessInformation FormatGuessInfo(string guess)
        {
            var guessTemplate = Regex.Split(guess.Trim(), @"\s+").LastOrDefault();

            var commandFormat = guess.Replace(guessTemplate, "{0}");

            return new FormattedGuessInformation(guessTemplate, commandFormat);
        }
    }
}
