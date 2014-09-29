using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Paths.Processors;
using CodeOwls.PowerShell.Provider;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    [CmdletProvider( "BIPS", ProviderCapabilities.ShouldProcess)]
    public class BipsProvider : Provider
    {
        BipsDrive Drive
        {
            get { return this.PSDriveInfo as BipsDrive; }
        }

        private static bool _initializedDefaultRunspace = false;

        protected override IPathNodeProcessor PathNodeProcessor
        {
            get
            {
                if (! _initializedDefaultRunspace)
                {
                    Runspace.DefaultRunspace.AvailabilityChanged += (sender, args) =>
                                                                    {
                                                                        if (args.RunspaceAvailability ==
                                                                            RunspaceAvailability.Available)
                                                                        {
                                                                            PathCache.Clear();
                                                                        }
                                                                    };
                    _initializedDefaultRunspace = true;
                }

                return new PathNodeProcessor(Drive);
            }
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            var bipsDrive = drive as BipsDrive;
            if (null != bipsDrive)
            {
                return bipsDrive;
            }

            return new BipsDrive( drive );
        }

        protected override ProviderInfo Start(ProviderInfo providerInfo)
        {
            this.SessionState.PSVariable.Set( "SSISApplication", new Application() );
            return base.Start(providerInfo);
        }
    }
}
