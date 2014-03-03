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
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using CodeOwls.PowerShell.Host.Utility;

namespace CodeOwls.PowerShell.Host.Configuration
{
    public class ShellConfiguration
    {
        public ShellConfiguration()
        {
            UISettings = new UISettings();
            InitialVariables = new List<PSVariable>();
            Cmdlets = new List<CmdletConfigurationEntry>();
            UnsupportedConsoleApplicationConfiguration = new UnsupportedConsoleApplicationConfiguration();
        }

        public string ShellName { get; set; }
        public Version ShellVersion { get; set; }
        public List<PSVariable> InitialVariables { get; set; }
        public UISettings UISettings { get; set; }
        public RunspaceConfiguration RunspaceConfiguration { get; set; }
        public IProfileInfo ProfileScripts { get; set; }
        public List<CmdletConfigurationEntry> Cmdlets { get; set; }
        public UnsupportedConsoleApplicationConfiguration UnsupportedConsoleApplicationConfiguration { get; set; }

        public bool IsUnsupportedConsoleApplication(string script, out Exception exception)
        {
            exception = null;
            if (null == UnsupportedConsoleApplicationConfiguration)
            {
                return false;
            }
            return UnsupportedConsoleApplicationConfiguration.IsUnsupportedConsoleApplication(script, out exception);
        }
    }
}
