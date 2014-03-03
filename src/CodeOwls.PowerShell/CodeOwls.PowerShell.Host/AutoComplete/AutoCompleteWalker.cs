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

namespace CodeOwls.PowerShell.Host.AutoComplete
{
    internal class AutoCompleteWalker : IAutoCompleteWalker
    {
        private readonly List<IAutoCompleteProvider> _providers;
        private string _currentGuess;
        private List<string> _currentSuggestions;
        private int _index;

        public AutoCompleteWalker(List<IAutoCompleteProvider> providers)
        {
            _providers = providers;
        }

        #region IAutoCompleteWalker Members

        public string NextUp(string guess)
        {
            bool isReset = LoadSuggestionsForGuess(guess);

            if (! _currentSuggestions.Any())
            {
                return null;
            }

            if (! isReset)
            {
                ++_index;

                if (_index > _currentSuggestions.Count - 1)
                {
                    _index = 0;
                }
            }

            return _currentSuggestions[_index];
        }

        public string NextDown(string guess)
        {
            bool isReset = LoadSuggestionsForGuess(guess);

            if (!_currentSuggestions.Any())
            {
                return null;
            }

            if (!isReset)
            {
                --_index;
                if (_index < 0)
                {
                    _index = _currentSuggestions.Count - 1;
                }

            }

            return _currentSuggestions[_index];
        }

        #endregion

        public void Reset()
        {
            _currentGuess = null;
        }


        private bool LoadSuggestionsForGuess(string guess)
        {
            if (StringComparer.InvariantCultureIgnoreCase.Equals(guess, _currentGuess))
            {
                return false;
            }

            ReloadSuggestionsForGuess(guess);

            return true;
        }

        private void ReloadSuggestionsForGuess(string guess)
        {
            if (null == _providers || !_providers.Any())
            {
                return;
            }

            _currentGuess = guess;
            _index = 0;
            _currentSuggestions = new List<string>();
            _providers.ForEach(
                p => _currentSuggestions.AddRange(p.GetSuggestions(guess))
                );
            _currentSuggestions.Sort(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
