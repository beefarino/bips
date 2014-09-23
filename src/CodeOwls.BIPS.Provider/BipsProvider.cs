using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;
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
        protected override IPathNodeProcessor PathNodeProcessor
        {
            get { return new PathNodeProcessor(Drive); }
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
