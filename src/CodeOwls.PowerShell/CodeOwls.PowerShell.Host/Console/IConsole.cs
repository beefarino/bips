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
using System.Management.Automation.Host;
using System.Threading;
using CodeOwls.PowerShell.Host.AutoComplete;
using CodeOwls.PowerShell.Host.Configuration;
using CodeOwls.PowerShell.Host.History;

namespace CodeOwls.PowerShell.Host.Console
{
    public interface IConsole
    {
        IAutoCompleteWalker AutoCompleteWalker { get; set; }
        IHistoryStackWalker HistoryStackWalker { get; set; }
        WaitHandle CommandEnteredEvent { get; }
        int EndOfLinePosition { get; }
        ConsoleSize ConsoleSizeInCharacters { get; }
        bool IsInputEntryModeEnabled { get; set; }
        bool KeyAvailable { get; }
        ConsoleColor ConsoleForeColor { get; set; }
        ConsoleColor ConsoleBackColor { get; set; }
        void Apply(UISettings settings);
        void ClearBuffer();
        ConsoleKeyInfo ReadNextKey();
        void FlushInputBuffer();
        void WritePrompt(string str);
        void Write(string str);
        void Write(string str, ConsoleColor fore, ConsoleColor back);
        void WriteLine(string value);
        void WriteErrorLine(string msg);
        void WriteWarningLine(string msg);
        void WriteDebugLine(string msg);
        void WriteVerboseLine(string msg);
        string ReadLine();
        IntPtr GetSafeWindowHandle();
    }
}
