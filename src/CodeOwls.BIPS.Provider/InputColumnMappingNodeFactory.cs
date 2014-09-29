using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

namespace CodeOwls.BIPS
{

    public class InputColumnMapping
    {
        public InputColumnMapping(IDTSInputColumn100 inputColumn, IDTSExternalMetadataColumn100 outputColumn)
        {
            InputColumn = inputColumn;
            OutputColumn = outputColumn;
        }

        public IDTSInputColumn100 InputColumn
        {
            get;
            private set;
        }

        public IDTSExternalMetadataColumn100 OutputColumn
        {
            get;
            private set;
        }

        public string InputColumnName
        {
            get { return null == InputColumn ? "<ignore>" : InputColumn.Name; }
        }

        public string OuputColumnName
        {
            get { return null == OutputColumn ? (string)null : OutputColumn.Name; }
        }
    }


    public class InputColumnMappingNodeFactory : NodeFactoryBase
    {
        private readonly InputColumnMapping _input;

        public InputColumnMappingNodeFactory(InputColumnMapping input)
        {
            _input = input;
        }
        
        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _input, Name );
        }

        public override string Name
        {
            get { return null == _input.InputColumn ? "<ignore>" : _input.InputColumn.Name; }
        }
    }
}