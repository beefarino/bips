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

        /*protected override System.Collections.ObjectModel.Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drives = new Collection<PSDriveInfo>();
            drives.Add(new BipsDrive(new PSDriveInfo("BIPS", ProviderInfo, "talon-sql-2012\\", "BIPS DRIVE", null)));
            return drives;
        }*/

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            var bipsDrive = drive as BipsDrive;
            if (null != bipsDrive)
            {
                return bipsDrive;
            }

            return new BipsDrive( drive );
        }
    }

    /*public class BipsPackageDrive : Drive
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
            return BipsDrive.Application.LoadPackage(PackagePath, null);
        }
    }
    
*/}
