using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class PackageInfoNodeFactory : NodeFactoryBase
    {
        private readonly PackageInfo _package;
        private readonly PackageInfo _parentPackage;

        public PackageInfoNodeFactory( PackageInfo package ) : this( package,null)
        {
            
        }
        public PackageInfoNodeFactory( PackageInfo package, PackageInfo parentPackage )
        {
            _package = package;
            _parentPackage = parentPackage;
        }

        bool IsFolder
        {
            get { return _package.Flags == DTSPackageInfoFlags.Folder; }
        }

        bool IsPackage
        {
            get { return _package.Flags == DTSPackageInfoFlags.Package; }
        }

        string FolderPath
        {
            get
            {
                var folder = _package.Folder;
                var name = _package.Name;
                var path = folder + "\\" + name;
                return path;
            }
        }
        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            if (IsPackage)
            {
                return null;
            }

            var app = new Application();
            var folderPath = FolderPath;

            //var packages = app.GetPackageInfos(folderPath, "talon-sql-2012", null, null);
            var packages = app.GetDtsServerPackageInfos(folderPath, "talon-sql-2012");
            var factories = (from PackageInfo package in packages select new PackageInfoNodeFactory(package)).Cast<INodeFactory>().ToList();
            return factories;
        }

        public override IPathNode GetNodeValue()
        {
            var nodeValue = PSObject.AsPSObject(_package);
            nodeValue.Properties.Add( new PSNoteProperty( "IsFolder", IsFolder));
            nodeValue.Properties.Add(new PSNoteProperty("IsPackage", IsPackage));
            nodeValue.Properties.Add(new PSNoteProperty("FolderPath", FolderPath));
            if (IsPackage)
            {
                //var p = BipsRootNodeFactory.Application.LoadFromSqlServer(FolderPath, "talon-sql-2012", null,null, null);
            }
            var node = IsPackage ? (IPathNode) new LeafPathNode(nodeValue, Name) : (IPathNode) new ContainerPathNode(nodeValue, Name);
            return node;
        }

        public override string Name
        {
            get { return _package.Name; }
        }
    }
}