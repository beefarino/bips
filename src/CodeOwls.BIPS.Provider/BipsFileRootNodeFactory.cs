using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using System.Management.Automation;

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
            ProgressRecord progress = new ProgressRecord(9, "Package Cache", "Loading package:");
            
            if (File.Exists(_filePath))
            {
                var fileInfo = new FileInfo(_filePath);
                var package = Drive.PackageCache.GetPackage(fileInfo.FullName, context, progress);

                nodes.Add(new CollectionNodeFactory<PackageDescriptor>("Packages",
                    new[] {new PackageDescriptor(package, fileInfo.FullName)}, a => new PackageNodeFactory(a)));
            }
            else if (Directory.Exists(_filePath))
            {
                var info = new DirectoryInfo(_filePath);
                var packagePaths = info.GetFiles("*.dtsx", SearchOption.TopDirectoryOnly);
                int count = packagePaths.Count();
                int i = 0;
                var packages = packagePaths.ToList().ConvertAll(e =>
                {
                    progress.PercentComplete = 100 * i++ / count;
                    progress.RecordType = ProgressRecordType.Processing;
                    return Drive.PackageCache.GetPackage(e.FullName, context, progress);
                });

                nodes.Add(new CollectionNodeFactory<PackageDescriptor>("Packages", packages, a => new PackageNodeFactory(a)));
            }

            progress.RecordType = ProgressRecordType.Completed;
            progress.PercentComplete = 100;
            context.WriteProgress(progress);
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