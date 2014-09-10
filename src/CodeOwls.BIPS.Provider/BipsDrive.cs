using System.Management.Automation;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class BipsDrive : Drive
    {
        private readonly string _server;
        private readonly SsisDbHelper _helper;
        private readonly PackageCache _cache;
        private static readonly Application _application;

        static BipsDrive()
        {
            _application = new Application();
        }

        public BipsDrive(PSDriveInfo driveInfo) : base(driveInfo)
        {
            _server = driveInfo.Root.TrimEnd('\\','/');
            _helper = new SsisDbHelper(_server);
            _cache = new PackageCache(_server);

            _cache.Clear();
        }

        internal SsisDbHelper SsisDbHelper { get { return _helper; } }
        internal PackageCache PackageCache { get { return _cache; } }
        
        public Application Application
        {
            get { return _application; }
        }

        public string Server
        {
            get { return _server; }
        }
    }
}