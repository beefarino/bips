using System.Collections.Generic;
using System.Linq;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS
{
    public class SsisFolderNodeFactory : NodeFactoryBase
    {
        private readonly BipsDrive _drive;
        private readonly SsisDbFolderDescriptor _folder;


        public SsisFolderNodeFactory( BipsDrive drive, SsisDbFolderDescriptor folder )
        {
            _drive = drive;
            _folder = folder;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            return
                _drive.SsisDbHelper.GetProjectsForFolder(_folder)
                      .ToList()
                      .ConvertAll(p => new SsisProjectNodeFactory(_drive, _folder, p));
        }
        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _folder, Name );
        }

        public override string Name
        {
            get { return _folder.Name; }
        }
    }
}