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

namespace CodeOwls.PowerShell.Host.Console
{
    [Flags]
    public enum ConsoleControlKeyStates
    {
        None = 0x000, 
        
        RightAltPressed = 0x001,
        LeftAltPressed      = 0x002,
        RightCtrlPressed    = 0x004,
        LeftCtrlPressed     = 0x008,
        ShiftPressed        = 0x010,
        NumLockOn           = 0x020,
        ScrollLockOn        = 0x040,
        CapsLockOn          = 0x080,
        EnhancedKey         = 0x100,
        
    }
}
