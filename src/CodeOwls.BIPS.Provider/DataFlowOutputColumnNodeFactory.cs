using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace CodeOwls.BIPS
{
    public class DataFlowOutputColumnNodeFactory : NodeFactoryBase
    {
        private readonly IDTSOutputColumn100 _outputColumn;

        public DataFlowOutputColumnNodeFactory(IDTSOutputColumn100 outputColumn)
        {
            _outputColumn = outputColumn;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            nodes.Add(new CollectionNodeFactory<IDTSCustomProperty100>("Properties", _outputColumn.CustomPropertyCollection.Cast<IDTSCustomProperty100>(), c => new DataFlowPropertyNodeFactory(c)));

            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( new BipsProxyIDTSOutputColumn100(_outputColumn), Name);
        }

        public override string Name
        {
            get { return _outputColumn.Name; }
        }
    }
}