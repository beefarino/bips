using System.Collections.Generic;
using System.Linq;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS
{
    public class SsisProjectNodeFactory : NodeFactoryBase
    {        
        private readonly BipsDrive _drive;
        private readonly SsisDbProjectDescriptor _project;
        
        public SsisProjectNodeFactory(BipsDrive drive, SsisDbProjectDescriptor project)
        {
            _drive = drive;
            _project = project;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var packageFiles = _drive.PackageCache.GetLocalPackageFilePathsForProject(_project.Path);

            return
                packageFiles.ToList()
                            .ConvertAll(f => _drive.Application.LoadPackage(f, null))
                            .ConvertAll(p => new PackageNodeFactory( new PackageDescriptor( p, _project.Name )));

        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _project, Name );
        }

        public override string Name
        {
            get { return _project.Name; }
        }
    }
}