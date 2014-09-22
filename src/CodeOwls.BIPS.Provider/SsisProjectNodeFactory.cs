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
        private readonly SsisDbFolderDescriptor _folder;
        private readonly SsisDbProjectDescriptor _project;
        
        public SsisProjectNodeFactory(BipsDrive drive, SsisDbFolderDescriptor folder, SsisDbProjectDescriptor project)
        {
            _drive = drive;
            _folder = folder;
            _project = project;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var packageFiles = _drive.PackageProxy.GetLocalPackageFilePathsForProject(_project.Path);

            var nodeFactories = from string packageFile in packageFiles.ToList()
                let f = _drive.Application.LoadPackage(packageFile, null )
                let pd = new PackageDescriptor( f, _project, packageFile )
                select new PackageNodeFactory( pd );

            return nodeFactories.ToList();
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