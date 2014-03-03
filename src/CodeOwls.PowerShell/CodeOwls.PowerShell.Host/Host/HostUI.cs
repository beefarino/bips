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
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;
using CodeOwls.PowerShell.Host.Configuration;
using CodeOwls.PowerShell.Host.Console;
using CodeOwls.PowerShell.Host.Utility;
using ConsoleKeyInfo = CodeOwls.PowerShell.Host.Console.ConsoleKeyInfo;

namespace CodeOwls.PowerShell.Host.Host
{
    internal class HostUI : PSHostUserInterface
    {
        private readonly IConsole _control;
        private readonly PSHostRawUserInterface _rawUI;
        private readonly UISettings _settings;

        public HostUI(IConsole control, UISettings settings, PSHostRawUserInterface rawUi)
        {
            _control = control;
            _settings = settings;
            _rawUI = rawUi;
        }

        public override PSHostRawUserInterface RawUI
        {
            get { return _rawUI; }
        }

        public event EventHandler<ProgressRecordEventArgs> Progress;

        public override string ReadLine()
        {
            _control.WritePrompt(String.Empty);
            _control.CommandEnteredEvent.WaitOne();
            return _control.ReadLine();
        }

        public override void Write(string value)
        {
            _control.WriteLine(value);
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            _control.Write(value, foregroundColor, backgroundColor);
        }

        public override void WriteLine(string value)
        {
            _control.WriteLine(value);
        }

        public override void WriteErrorLine(string message)
        {
            _control.WriteErrorLine(message);
        }

        public override void WriteDebugLine(string value)
        {
            _control.WriteDebugLine(value);
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            var ev = Progress;
            if (null == ev)
            {
                return;
            }

            ev(this, new ProgressRecordEventArgs(sourceId, record));
        }

        public override void WriteVerboseLine(string message)
        {
            _control.WriteVerboseLine(message);
        }

        public override void WriteWarningLine(string message)
        {
            _control.WriteWarningLine(message);
        }

        public override Dictionary<string, PSObject> Prompt(string caption, string message,
                                                            Collection<FieldDescription> descriptions)
        {
            _control.WriteLine(String.Empty);

            if( ! String.IsNullOrEmpty( caption ) )
            {
                _control.WriteLine(caption);
            }
            if (!String.IsNullOrEmpty(message))
            {
                _control.WriteLine(message);
            }

            var results = new Dictionary<string, PSObject>();

            descriptions.ToList().ForEach(
                d =>
                    {
                        bool isSecure = d.ParameterTypeFullName == typeof (SecureString).FullName;

                        var label = String.IsNullOrEmpty( d.Label ) ? d.Name : d.Label;
                        var prompt = (label + ": ").Replace("&", String.Empty);
                        _control.WritePrompt(prompt);
                        
                        if (isSecure)
                        {
                            results[d.Name] = ReadLineAsSecureString().ToPSObject();
                        }
                        else
                        {
                            _control.CommandEnteredEvent.WaitOne();
                            results[d.Name] = _control.ReadLine().Trim().ToPSObject();                            
                        }
                    }
                );

            return results;
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName,
                                                         string targetName)
        {
            return PromptForCredential(caption, message, userName, targetName, PSCredentialTypes.Default,
                                       PSCredentialUIOptions.Default);
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName,
                                                         string targetName, PSCredentialTypes allowedCredentialTypes,
                                                         PSCredentialUIOptions options)
        {
            try
            {
                if (_settings.PromptForCredentialsInConsole)
                {
                    return PromptForCredentialFromConsole(caption, message, userName, targetName, allowedCredentialTypes,
                                                          options);
                }
            }
            catch
            {
            }

            IntPtr handle = _control.GetSafeWindowHandle();
            return NativeUtils.CredUIPromptForCredential(
                caption,
                message,
                userName,
                targetName,
                allowedCredentialTypes,
                options,
                handle);
        }

        private PSCredential PromptForCredentialFromConsole(string caption, string message, string userName,
                                                            string targetName, PSCredentialTypes allowedCredentialTypes,
                                                            PSCredentialUIOptions options)
        {
            _control.WriteWarningLine(caption);
            _control.WriteLine(message);

            var baseMoniker = String.IsNullOrEmpty(targetName) ? "{1}" : "{0}\\{1}";

            var moniker = String.Format(baseMoniker, targetName, userName);
            var prompt = "Password for user " + moniker + ": ";
            _control.WritePrompt(prompt);

            var pwd = ReadLineAsSecureString();

            return new PSCredential(moniker, pwd);
        }

        public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices,
                                            int defaultChoice)
        {
            _control.WriteWarningLine(caption);
            _control.WriteLine(message);

            while (true)
            {
                int choiceIndex = 0;
                string defaultChoicePrompt = String.Empty;
                List<char> hotkeys = new List<char>();
                choices.ToList().ForEach(
                    choice =>
                        {
                            var index = choice.Label.IndexOf('&') + 1;
                            if (-1 == index || index > (choice.Label.Length - 1))
                            {
                                return;
                            }
                            var c = choice.Label.ToUpperInvariant()[index];
                            hotkeys.Add(c);

                            ConsoleColor color = _settings.ForegroundColor;
                            ConsoleColor back = _settings.BackgroundColor;

                            if (choiceIndex == defaultChoice)
                            {
                                color = _settings.WarningForegroundColor;
                                back = _settings.WarningBackgroundColor;
                                defaultChoicePrompt = String.Format(" (default is \"{0}\")", c);
                            }

                            _control.Write(
                                String.Format("[{0}]", c),
                                color,
                                _control.ConsoleBackColor);

                            _control.Write(String.Format(" {0}  ", choice.Label.Replace("&", String.Empty)), color, back);
                            ++choiceIndex;
                        }
                    );

                _control.WritePrompt(String.Format(" [?] Help{0}:", defaultChoicePrompt));

                _control.CommandEnteredEvent.WaitOne();

                var line = _control.ReadLine().Trim();
                if (line.Length == 0)
                {
                    if (-1 == defaultChoice)
                    {
                        continue;
                    }
                    return defaultChoice;
                }

                char charChoice = line.ToUpperInvariant()[0];

                if ('?' == charChoice)
                {
                    choices.ToList().ForEach(
                        choice => _control.WriteLine(
                            String.Format(
                                " {0} - {1}",
                                choice.Label.Replace("&", String.Empty),
                                choice.HelpMessage)
                                      ));
                    continue;
                }

                choiceIndex = hotkeys.IndexOf(charChoice);
                if (-1 == choiceIndex)
                {
                    continue;
                }
                return choiceIndex;
            }
        }

        public override SecureString ReadLineAsSecureString()
        {
            _control.IsInputEntryModeEnabled = false;
            try
            {
                SecureString str = new SecureString();

                ConsoleKeyInfo keyInfo;
                char[] eol = new[] {'\r', '\n'};
                while (!eol.Contains((keyInfo = _control.ReadNextKey()).Character))
                {
                    str.AppendChar(keyInfo.Character);
                    _control.Write("*");
                }
                str.MakeReadOnly();

                return str;
            }
            finally
            {
                _control.IsInputEntryModeEnabled = true;
                _control.WritePrompt(String.Empty);
                _control.WriteLine(String.Empty);
                _control.ReadLine();
            }
        }
    }
}
