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

namespace CodeOwls.PowerShell.Host.Configuration
{
    public class UISettings
    {
        public ConsoleColor BackgroundColor;
        public ConsoleColor DebugBackgroundColor;
        public ConsoleColor DebugForegroundColor;
        public ConsoleColor ErrorBackgroundColor;
        public ConsoleColor ErrorForegroundColor;

        public string FontName;
        public int FontSize;
        public ConsoleColor ForegroundColor;

        public bool PromptForCredentialsInConsole;
        public ConsoleColor VerboseBackgroundColor;
        public ConsoleColor VerboseForegroundColor;
        public ConsoleColor WarningBackgroundColor;
        public ConsoleColor WarningForegroundColor;

        public UISettings()
        {
            BackgroundColor =
                VerboseBackgroundColor =
                DebugBackgroundColor =
                WarningBackgroundColor =
                ErrorBackgroundColor =
                ConsoleColor.Black;

            ForegroundColor = ConsoleColor.White;
            ErrorForegroundColor = ConsoleColor.Red;
            WarningForegroundColor = ConsoleColor.Yellow;
            VerboseForegroundColor = ConsoleColor.Green;
            DebugForegroundColor = ConsoleColor.Cyan;

            FontName = "Courier New";
            FontSize = 10;

            PromptForCredentialsInConsole = true;
        }
    }
}
