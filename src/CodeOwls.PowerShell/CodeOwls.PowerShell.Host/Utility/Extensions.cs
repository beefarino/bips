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
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Text;
using CodeOwls.PowerShell.Host.Console;

namespace CodeOwls.PowerShell.Host.Utility
{
    public static class Extensions
    {
        public static ControlKeyStates ToControlKeyState( this Console.ConsoleControlKeyStates state )
        {
            var map = new Dictionary<Console.ConsoleControlKeyStates, ControlKeyStates>
                          {
                              {ConsoleControlKeyStates.CapsLockOn, ControlKeyStates.CapsLockOn},
                              {ConsoleControlKeyStates.EnhancedKey, ControlKeyStates.EnhancedKey},
                              {ConsoleControlKeyStates.LeftAltPressed, ControlKeyStates.LeftAltPressed},
                              {ConsoleControlKeyStates.LeftCtrlPressed, ControlKeyStates.LeftCtrlPressed},
                              {ConsoleControlKeyStates.NumLockOn, ControlKeyStates.NumLockOn},
                              {ConsoleControlKeyStates.RightAltPressed, ControlKeyStates.RightAltPressed},
                              {ConsoleControlKeyStates.RightCtrlPressed, ControlKeyStates.RightCtrlPressed},
                              {ConsoleControlKeyStates.ScrollLockOn, ControlKeyStates.ScrollLockOn},
                              {ConsoleControlKeyStates.ShiftPressed, ControlKeyStates.ShiftPressed},
                          };

            ControlKeyStates controlKeyState = 0;
            map.ToList().ForEach(pair =>
                                     {
                                         if (0 != (pair.Key & state))
                                         {
                                             controlKeyState |= pair.Value;
                                         }
                                     });
            return controlKeyState;
        }

        public static KeyInfo ToKeyInfo(this Console.ConsoleKeyInfo cki)
        {
            return new KeyInfo( cki.VirtualKeyCode, cki.Character, cki.ControlKeyState.ToControlKeyState(), cki.KeyDown );
        }
        
        public static PSObject ToPSObject(this object o)
        {
            return PSObject.AsPSObject(o);
        }

        public static string ToPSHashtable(this IProfileInfo p)
        {
            return
                String.Format(
                    @"@{{
                AllUsersAllHosts = '{0}';
                AllUsersCurrentHost = '{1}';
                AllUsersPowerShellHost = '{2}';
                CurrentUserAllHosts = '{3}';
                CurrentUserCurrentHost = '{4}';
                CurrentUserPowerShellHost = '{5}'}}",
                    p.AllUsersAllHosts,
                    p.AllUsersCurrentHost,
                    p.AllUsersPowerShellHost,
                    p.CurrentUserAllHosts,
                    p.CurrentUserCurrentHost,
                    p.CurrentUserPowerShellHost);
        }
        
        public static string ToDotSource(this FileInfo fileInfo)
        {
            return String.Format(". \"{0}\"", fileInfo.FullName).EscapeTicks();
        }

        public static string EscapeTicks(this string str)
        {
            StringBuilder builder = new StringBuilder(str.Length*2);
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                switch (ch)
                {
                    case '`':
                    case '\'':
                        builder.Append('`');
                        break;
                }
                builder.Append(ch);
            }
            string format = builder.ToString();
            return format;
        }
    }
}
