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
namespace CodeOwls.PowerShell.Host.Console
{
    public struct ConsoleKeyInfo
    {
        public char Character { get; set; }
        public ConsoleControlKeyStates ControlKeyState { get; set; }
        public bool KeyDown { get; set; }
        public int VirtualKeyCode { get; set; }
    }
}
