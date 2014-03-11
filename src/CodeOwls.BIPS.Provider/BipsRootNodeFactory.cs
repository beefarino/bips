using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class BipsRootNodeFactory : NodeFactoryBase
    {
        private static readonly Application _application = new Application();
        private readonly string _serverName;


        public BipsRootNodeFactory(string serverName)
        {
            _serverName = serverName;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {               
            var nodes = new List<INodeFactory>();

            var connectionInfos = Application.ConnectionInfos.Cast<ConnectionInfo>().ToList();
            nodes.Add( new CollectionNodeFactory<ConnectionInfo>( "Connections", connectionInfos, a=>new DtsNameObjectNodeFactory(a) ));

            var typeInfos = Application.DataTypeInfos.Cast<DataTypeInfo>().ToList();
            nodes.Add(new CollectionNodeFactory<DataTypeInfo>("DataTypes", typeInfos, a => new DataTypeInfoObjectNodeFactory(a)));

            var dbInfos = Application.DBProviderInfos.Cast<DBProviderInfo>().ToList();
            nodes.Add(new CollectionNodeFactory<DBProviderInfo>("DbProviders", dbInfos, a => new DbProviderInfoNodeFactory(a)));

            var logInfos = Application.LogProviderInfos.Cast<LogProviderInfo>().ToList();
            nodes.Add(new CollectionNodeFactory<LogProviderInfo>("LogProviders", logInfos, a=>new LogProviderInfoNodeFactory(a)));
            
            var packageInfos = Application.GetDtsServerPackageInfos("", _serverName); 
            var packages = packageInfos.Cast<PackageInfo>();
            nodes.Add(new CollectionNodeFactory<PackageInfo>("SSIS", packages, a => new PackageInfoNodeFactory(a)));

            packageInfos = Application.GetPackageInfos("", _serverName, null, null);
            packages = packageInfos.Cast<PackageInfo>();
            nodes.Add(new CollectionNodeFactory<PackageInfo>("SQLServer", packages, a => new PackageInfoNodeFactory(a)));

            var folders = new SsisDbHelper(_serverName).Folders;
            nodes.Add(new CollectionNodeFactory<SsisDbFolderDescriptor>("SSISDB", folders, a => new SSISFolderNodeFactory(_serverName, a)));


            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( Application, Name );
        }

        public override string Name
        {
            get { return _serverName; }
        }

        public static Application Application
        {
            get { return _application; }
        }
    }
}