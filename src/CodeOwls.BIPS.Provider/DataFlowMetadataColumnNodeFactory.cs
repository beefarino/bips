using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace CodeOwls.BIPS
{
    public class DataFlowMetadataColumnNodeFactory : NodeFactoryBase
    {
        private readonly IDTSExternalMetadataColumn100 _input;

        public DataFlowMetadataColumnNodeFactory(IDTSExternalMetadataColumn100 input)
        {
            _input = input;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            nodes.Add(new CollectionNodeFactory<IDTSCustomProperty100>("Properties", _input.CustomPropertyCollection.Cast<IDTSCustomProperty100>(), c => new DataFlowPropertyNodeFactory(c)));
        
            return nodes;
        }
        
        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _input, Name );
        }

        public override string Name
        {
            get { return _input.Name; }
        }
    }
}