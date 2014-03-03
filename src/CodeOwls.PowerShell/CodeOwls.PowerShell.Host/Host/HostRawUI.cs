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
using CodeOwls.PowerShell.Host.Console;
using CodeOwls.PowerShell.Host.Utility;

namespace CodeOwls.PowerShell.Host.Host
{
    internal class HostRawUI : PSHostRawUserInterface
    {
        private readonly IConsole _control;
        private readonly string _title;

        public HostRawUI(IConsole control, string title)
        {
            _control = control;
            _title = title;
        }

        public override ConsoleColor ForegroundColor
        {
            get { return _control.ConsoleForeColor; }
            set { _control.ConsoleForeColor = value; }
        }

        public override ConsoleColor BackgroundColor
        {
            get { return _control.ConsoleBackColor; }
            set { _control.ConsoleBackColor = value; }
        }

        public override Coordinates CursorPosition
        {
            get { return new Coordinates(); }
            set { }
        }

        public override Coordinates WindowPosition
        {
            get { return new Coordinates(); }
            set { }
        }

        public override int CursorSize
        {
            get { return 0; }
            set { }
        }

        public override Size BufferSize
        {
            get
            {                
                return new Size(
                    _control.ConsoleSizeInCharacters.Width,
                    9999
                    );
            }
            set { }
        }

        public override Size WindowSize
        {
            get
            {
                var s = _control.ConsoleSizeInCharacters;
                return new Size(
                    s.Width, s.Height
                    );
            }
            set { }
        }

        public override Size MaxWindowSize
        {
            get { return WindowSize; }
        }

        public override Size MaxPhysicalWindowSize
        {
            get { return MaxWindowSize; }
        }

        public override bool KeyAvailable
        {
            get { return _control.KeyAvailable; }
        }

        public override string WindowTitle
        {
            get { return _title; }
            set { }
        }

        public override KeyInfo ReadKey(ReadKeyOptions options)
        {
            var key = _control.ReadNextKey();

            if (0 == (ReadKeyOptions.NoEcho & options))
            {
                _control.Write(key.Character.ToString());
            }
            return key.ToKeyInfo();
        }

        public override void FlushInputBuffer()
        {
            _control.FlushInputBuffer();
        }

        public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
        {
        }

        public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
        {
            if (rectangle.Left == rectangle.Right &&
                rectangle.Left == rectangle.Bottom &&
                rectangle.Left == rectangle.Top &&
                rectangle.Left == (-1))
            {
                _control.ClearBuffer();
            }
        }

        public override BufferCell[,] GetBufferContents(Rectangle rectangle)
        {
            return null;
        }

        public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip,
                                                  BufferCell fill)
        {
        }
    }
}
