using System.IO;
using System.Management.Automation;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class BipsDrive : Drive
    {
        private readonly string _driveRoot;
        private readonly SsisDbHelper _helper;
        private readonly PackageCache _cache;
        private readonly ServerPackageProxy _proxy;
        private static readonly Application _application;

        static BipsDrive()
        {
            _application = new Application();
        }

        public BipsDrive(PSDriveInfo driveInfo) : base(driveInfo)
        {
            _driveRoot = driveInfo.Root;
            _helper = new SsisDbHelper(_driveRoot);
            _cache = new PackageCache( this );

            if (File.Exists(_driveRoot) || Directory.Exists(_driveRoot))
            {
                return;
            }

            _proxy = new ServerPackageProxy(_driveRoot); 
            _proxy.Clear();
        }

        internal SsisDbHelper SsisDbHelper { get { return _helper; } }
        internal ServerPackageProxy PackageProxy { get { return _proxy; } }
        internal PackageCache PackageCache { get { return _cache; } }

        public Application Application
        {
            get { return _application; }
        }

        public string DriveRoot
        {
            get { return _driveRoot; }
        }
    }
}