using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;
using CodeOwls.PowerShell.Paths.Processors;
using CodeOwls.PowerShell.Provider;
using CodeOwls.PowerShell.Provider.PathNodes;
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

        protected override System.Collections.ObjectModel.Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drives = new Collection<PSDriveInfo>();
            drives.Add( new BipsDrive( "talon-sql-2012", new PSDriveInfo("BIPS", ProviderInfo, "", "BIPS DRIVE", null)));
            return drives;
        }
    }

    public class BipsPackageDrive : Drive
    {
        private readonly string _packagePath;
        private Package _package;

        public BipsPackageDrive(string packagePath, PSDriveInfo driveInfo)
            : base(driveInfo)
        {
            _packagePath = packagePath;
        }

        public string PackagePath
        {
            get { return _packagePath; }
        }

        public Package Package
        {
            get
            {
                if (null == _package)
                {
                    _package = LoadPackage();
                }

                return _package;
            }
        }

        private Package LoadPackage()
        {
            return BipsRootNodeFactory.Application.LoadPackage(PackagePath, null);
        }
    }
    
    [CmdletProvider("BIPS.Package", ProviderCapabilities.ShouldProcess)]
    public class BipsPackageProvider : Provider
    {
        BipsPackageDrive Drive
        {
            get { return this.PSDriveInfo as BipsPackageDrive; }
        }

        protected override IPathNodeProcessor PathNodeProcessor
        {
            get { return new PathNodeProcessor(Drive); }
        }

        protected override System.Collections.ObjectModel.Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drives = new Collection<PSDriveInfo>();
            drives.Add(new BipsPackageDrive(@"c:\users\administrator\documents\package\package.dtsx", new PSDriveInfo("pkg", ProviderInfo, "", "BIPS PACKAGE DRIVE", null)));
            return drives;
        }
    }

    public class PathNodeProcessor : PathNodeProcessorBase
    {
        private readonly BipsPackageDrive _packageDrive;
        private readonly BipsDrive _drive;

        public PathNodeProcessor(BipsDrive drive)
        {
            _drive = drive;
        }

        public PathNodeProcessor(BipsPackageDrive packageDrive)
        {
            _packageDrive = packageDrive;            
        }

        protected override INodeFactory Root
        {
            get
            {
                if (null != _packageDrive)
                {
                    return new PackageNodeFactory(_packageDrive.Package);
                }
                return new BipsRootNodeFactory(_drive.Server);
            }
        }
    }
}
