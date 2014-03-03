using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;
using CodeOwls.PowerShell.Paths.Processors;
using CodeOwls.PowerShell.Provider;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS
{
    public class BipsDrive : Drive
    {
        private readonly string _server;

        public BipsDrive(string server, PSDriveInfo driveInfo) : base(driveInfo)
        {
            _server = server;
        }

        public string Server
        {
            get { return _server; }
        }
    }

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

        protected override System.Collections.ObjectModel.Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drives = new Collection<PSDriveInfo>();
            drives.Add( new BipsDrive( "talon-sql-2012", new PSDriveInfo("BIPS", ProviderInfo, "", "BIPS DRIVE", null)));
            return drives;
        }
    }

    public class PathNodeProcessor : PathNodeProcessorBase
    {
        private readonly BipsDrive _drive;

        public PathNodeProcessor(BipsDrive drive)
        {
            _drive = drive;
        }

        protected override INodeFactory Root
        {
            get { return new BipsRootNodeFactory(_drive.Server); }
        }
    }
}
