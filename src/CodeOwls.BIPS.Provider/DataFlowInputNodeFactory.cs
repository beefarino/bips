using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace CodeOwls.BIPS
{
    public class DataFlowInputNodeFactory : NodeFactoryBase
    {
        private readonly IDTSInput100 _input;

        public DataFlowInputNodeFactory(IDTSInput100 input)
        {
            _input = input;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            nodes.Add(new CollectionNodeFactory<IDTSInputColumn100>("InputColumns", _input.InputColumnCollection.Cast<IDTSInputColumn100>(), c => new DataFlowInputColumnNodeFactory(c)));
            nodes.Add(new CollectionNodeFactory<IDTSCustomProperty100>("Properties", _input.CustomPropertyCollection.Cast<IDTSCustomProperty100>(), c => new DataFlowPropertyNodeFactory(c)));
            nodes.Add(new CollectionNodeFactory<IDTSExternalMetadataColumn100>("Metadata", 
                _input.ExternalMetadataColumnCollection.Cast<IDTSExternalMetadataColumn100>(), 
                c => new DataFlowMetadataColumnNodeFactory(c)));
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