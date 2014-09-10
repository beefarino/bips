using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace CodeOwls.BIPS
{
    public class DataFlowInputColumnNodeFactory : NodeFactoryBase
    {
        private readonly IDTSInputColumn100 _inputColumn;

        public DataFlowInputColumnNodeFactory(IDTSInputColumn100 inputColumn)
        {
            _inputColumn = inputColumn;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            nodes.Add(new CollectionNodeFactory<IDTSCustomProperty100>("Properties", _inputColumn.CustomPropertyCollection.Cast<IDTSCustomProperty100>(), c => new DataFlowPropertyNodeFactory(c)));

            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _inputColumn, Name );
        }

        public override string Name
        {
            get { return _inputColumn.Name; }
        }
    }
}