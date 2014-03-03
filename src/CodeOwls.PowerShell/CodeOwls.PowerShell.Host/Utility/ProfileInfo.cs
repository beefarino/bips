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
using System.Reflection;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace CodeOwls.PowerShell.Host.Utility
{
    public class ProfileInfo : IProfileInfo
    {
        private const string MicrosoftPowerShellId = "Microsoft.PowerShell";
        private const string PowerShellSubdirectory = "WindowsPowerShell";
        private const string ProfileFileName = "profile.ps1";
        private readonly string _shellHostProfilePath;
        private readonly string _shellId;

        public ProfileInfo(string shellId, string shellHostProfilePath)
        {
            _shellId = shellId;
            _shellHostProfilePath = shellHostProfilePath;
        }

        private static string MicrosftPowerShellPath
        {
            get
            {
                var registryPath = @"SOFTWARE\Microsoft\PowerShell\1\PowerShellEngine";
                using (var key = Registry.LocalMachine.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadSubTree,
                                                                  RegistryRights.QueryValues))
                {
                    if (null != key && key.GetValueNames().Contains("ApplicationBase"))
                    {
                        return key.GetValue("ApplicationBase", String.Empty).ToString();
                    }
                }

                Assembly asm = Assembly.GetAssembly(typeof (PSObject));
                return Path.GetDirectoryName(asm.Location);
            }
        }

        #region IProfileInfo Members

        public PSObject GetProfilePSObject()
        {
            PSObject v = new PSObject(CurrentUserCurrentHost);

            v.Properties.Add(new PSNoteProperty("AllUsersAllHosts", AllUsersAllHosts));
            v.Properties.Add(new PSNoteProperty("AllUsersCurrentHost", AllUsersCurrentHost));
            v.Properties.Add(new PSNoteProperty("CurrentUserAllHosts", CurrentUserAllHosts));
            v.Properties.Add(new PSNoteProperty("CurrentUserCurrentHost", CurrentUserCurrentHost));
            // v.Properties.Add(new PSNoteProperty("AllUsersPowerShellHost", AllUsersPowerShellHost));
            v.Properties.Add(new PSNoteProperty("CurrentUserPowerShellHost", CurrentUserPowerShellHost));

            return v;
        }

        public string AllUsersAllHosts
        {
            get { return GetMicrosoftPowerShellProfilePath(false, true); }
        }

        public string AllUsersPowerShellHost
        {
            get { return GetMicrosoftPowerShellProfilePath(false, false); }
        }

        public string CurrentUserAllHosts
        {
            get { return GetMicrosoftPowerShellProfilePath(true, true); }
        }

        public string CurrentUserPowerShellHost
        {
            get { return GetMicrosoftPowerShellProfilePath(true, false); }
        }

        public string AllUsersCurrentHost
        {
            get { return GetProfilePath(_shellHostProfilePath, null); }
        }

        public string CurrentUserCurrentHost
        {
            get { return GetCurrentUserProfilePath(_shellId, null); }
        }

        public IEnumerable<string> InRunOrder
        {
            get
            {
                return new[]
                           {
                               AllUsersAllHosts,
                               //AllUsersPowerShellHost,
                               CurrentUserAllHosts,
                               CurrentUserPowerShellHost,
                               AllUsersCurrentHost,
                               CurrentUserCurrentHost,
                           };
            }
        }

        #endregion

        private static string GetMicrosoftPowerShellProfilePath(bool isForCurrentUser, bool isForAllHosts)
        {
            if (isForCurrentUser)
            {
                return GetCurrentUserProfilePath(PowerShellSubdirectory, isForAllHosts ? null : MicrosoftPowerShellId);
            }

            return GetProfilePath(MicrosftPowerShellPath, isForAllHosts ? null : MicrosoftPowerShellId);
        }

        private static string GetCurrentUserProfilePath(string subdirectoryName, string shellId)
        {
            var rootPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            return GetProfilePath(Path.Combine(rootPath, subdirectoryName), shellId);
        }

        private static string GetProfilePath(string rootPath, string shellId)
        {
            string profileFileName = null;

            if (!String.IsNullOrEmpty(shellId))
            {
                profileFileName = String.Format("{0}_{1}", shellId, ProfileFileName);
            }
            else
            {
                profileFileName = ProfileFileName;
            }

            return Path.Combine(rootPath, profileFileName);
        }
    }
}
