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
using CodeOwls.PowerShell.Host.Executors;

namespace CodeOwls.PowerShell.Host.AutoComplete
{
    internal class ProviderPathAutoCompleteProvider : CommandAutoCompleteProvider
    {
        private const string Command =
            @"( resolve-path '{0}' | select -exp path ) -replace ([regex]::escape( $pwd.path +'\') +'?'),'.\'";


        public ProviderPathAutoCompleteProvider(Executor executor) : base(Command, executor)
        {
        }

        public override System.Collections.Generic.IEnumerable<string> GetSuggestions(string guess)
        {
            return base.GetSuggestions(guess);
        }
    }
}
