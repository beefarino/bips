using System.Management.Automation;
using CodeOwls.PowerShell.Provider;

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
}