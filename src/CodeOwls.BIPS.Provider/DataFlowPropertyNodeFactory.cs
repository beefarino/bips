using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace CodeOwls.BIPS
{
    public class DataFlowPropertyNodeFactory : NodeFactoryBase
    {
        private readonly IDTSCustomProperty100 _input;

        public DataFlowPropertyNodeFactory(IDTSCustomProperty100 input)
        {
            _input = input;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode(_input, Name);
        }

        public override string Name
        {
            get { return _input.Name; }
        }
    }
}