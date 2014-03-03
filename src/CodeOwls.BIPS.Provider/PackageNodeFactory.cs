using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class PackageNodeFactory : NodeFactoryBase
    {
        private readonly PackageInfo _package;
        private readonly PackageInfo _parentPackage;

        public PackageNodeFactory( PackageInfo package ) : this( package,null)
        {
            
        }
        public PackageNodeFactory( PackageInfo package, PackageInfo parentPackage )
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
                return folder + "\\" + name;
            }
        }
        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            if (IsPackage)
            {
                return null;
            }

            var app = BipsRootNodeFactory.Application;
            var folderPath = FolderPath;

            var packages = app.GetPackageInfos(folderPath, "talon-sql-2012", null, null);
            var factories = (from PackageInfo package in packages select new PackageNodeFactory(package)).Cast<INodeFactory>().ToList();
            return factories;
        }

        public override IPathNode GetNodeValue()
        {
            var nodeValue = PSObject.AsPSObject(_package);
            nodeValue.Properties.Add( new PSNoteProperty( "IsFolder", IsFolder));
            nodeValue.Properties.Add(new PSNoteProperty("IsPackage", IsPackage));
            nodeValue.Properties.Add(new PSNoteProperty("FolderPath", FolderPath));

            var node = IsPackage ? (IPathNode) new LeafPathNode(nodeValue, Name) : (IPathNode) new ContainerPathNode(nodeValue, Name);
            return node;
        }

        public override string Name
        {
            get { return _package.Name; }
        }
    }
}