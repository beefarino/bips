using System.Collections.Generic;
using System.IO;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS
{
    public class BipsFileRootNodeFactory : BipsRootNodeFactory
    {
        private readonly FileInfo _filePath;

        public BipsFileRootNodeFactory(BipsDrive drive, string filePath) : base( drive )
        {
            _filePath = new FileInfo(filePath);
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            GetCommonNodeFactories( nodes );

            var package = Drive.PackageCache.GetPackage(_filePath.FullName);

            nodes.Add(new CollectionNodeFactory<PackageDescriptor>("Packages", new[]{ new PackageDescriptor( package, _filePath.FullName)}, a => new PackageNodeFactory(a)));
            
            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode(_filePath, Name);
        }

        public override string Name
        {
            get { return _filePath.Name; }
        }
    }
}