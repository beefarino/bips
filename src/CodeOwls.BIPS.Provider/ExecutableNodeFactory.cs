using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class ExecutableNodeFactory : NodeFactoryBase
    {
        private readonly DtsContainer _executable;
        private readonly MainPipe _mainPipe;

        public ExecutableNodeFactory(Executable executable)
        {
            _executable = (DtsContainer)executable;
            
            var host = _executable as TaskHost;
            if (null != host)
            {
                _mainPipe = host.InnerObject as MainPipe;
            }
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            if (null == _mainPipe)
            {
                return null;
            }

            var nodes = new List<INodeFactory>();
            var metadata = _mainPipe.ComponentMetaDataCollection.Cast<IDTSComponentMetaData100>();

            nodes.AddRange(metadata.ToList().ConvertAll(c => new DataFlowComponentNodeFactory(c)));

            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _executable, Name);
        }

        public override string Name
        {
            get { return _executable.Name; }
        }
    }
}