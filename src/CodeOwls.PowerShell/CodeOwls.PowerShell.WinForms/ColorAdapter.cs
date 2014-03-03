
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
using System.Drawing;
using System.Linq;

namespace CodeOwls.PowerShell.WinForms.Utility
{
    public static class ColorAdapter
    {
        public static Color ToColor( this ConsoleColor clr )
        {
            return Adapt(clr, Color.White);
        }

        public static ConsoleColor ToConsoleColor( this Color clr )
        {
            return Adapt(clr, ConsoleColor.White);
        }

        private static readonly Dictionary<ConsoleColor, Color> ColorMap =
            new Dictionary<ConsoleColor, Color>
                {
                    {ConsoleColor.Black, Color.Black},
                    {ConsoleColor.Blue, Color.Blue},
                    {ConsoleColor.Cyan, Color.Cyan},
                    {ConsoleColor.DarkBlue, Color.DarkBlue},
                    {ConsoleColor.DarkCyan, Color.DarkCyan},
                    {ConsoleColor.DarkGray, Color.DarkGray},
                    {ConsoleColor.DarkGreen, Color.DarkGreen},
                    {ConsoleColor.DarkMagenta, Color.DarkMagenta},
                    {ConsoleColor.DarkRed, Color.DarkRed},
                    {ConsoleColor.DarkYellow, Color.DarkOrange},
                    {ConsoleColor.Gray, Color.Gray},
                    {ConsoleColor.Green, Color.Green},
                    {ConsoleColor.Magenta, Color.Magenta},
                    {ConsoleColor.Red, Color.Red},
                    {ConsoleColor.White, Color.White},
                    {ConsoleColor.Yellow, Color.Yellow}
                };

        static internal Color Adapt(ConsoleColor consoleColor, Color @default)
        {
            if (!ColorMap.ContainsKey(consoleColor))
            {
                return @default;
            }

            return ColorMap[consoleColor];
        }

        static internal ConsoleColor Adapt(Color color,ConsoleColor @default)
        {
            if (!ColorMap.ContainsValue(color))
            {
                return (from clr in ColorMap
                         let red = Math.Abs(color.R - clr.Value.R)
                         let blue = Math.Abs(color.B - clr.Value.B)
                         let green = Math.Abs(color.G - clr.Value.G)
                         let delta = red + blue + green
                         select new { Color = clr, Delta = delta }
                    ).OrderBy(t => t.Delta).First().Color.Key;
            }

            return (from pair in ColorMap where pair.Value == color select pair.Key).FirstOrDefault();
        }
    }
}
