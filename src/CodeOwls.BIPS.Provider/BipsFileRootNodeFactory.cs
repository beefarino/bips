using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS
{
    public class BipsFileRootNodeFactory : BipsRootNodeFactory
    {
        private readonly string _filePath;

        public BipsFileRootNodeFactory(BipsDrive drive, string filePath) : base( drive )
        {
            _filePath = filePath;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            GetCommonNodeFactories( nodes );

            if (File.Exists(_filePath))
            {
                var fileInfo = new FileInfo(_filePath);
                var package = Drive.PackageCache.GetPackage(fileInfo.FullName);

                nodes.Add(new CollectionNodeFactory<PackageDescriptor>("Packages",
                    new[] {new PackageDescriptor(package, fileInfo.FullName)}, a => new PackageNodeFactory(a)));
            }
            else if (Directory.Exists(_filePath))
            {
                var info = new DirectoryInfo(_filePath);
                var packagePaths = info.GetFiles("*.dtsx", SearchOption.TopDirectoryOnly);
                var packages = packagePaths.ToList().ConvertAll( e=> Drive.PackageCache.GetPackage(e.FullName) );

                nodes.Add(new CollectionNodeFactory<PackageDescriptor>("Packages", packages, a => new PackageNodeFactory(a)));
            }

            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode(_filePath, Name);
        }

        public override string Name
        {
            get { return _filePath; }
        }
    }
}