using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class DataTypeInfoObjectNodeFactory : NodeFactoryBase
    {
        private readonly DataTypeInfo _dataTypeInfo;

        public DataTypeInfoObjectNodeFactory(DataTypeInfo dataTypeInfo)
        {
            _dataTypeInfo = dataTypeInfo;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode(_dataTypeInfo, Name);
        }

        public override string Name
        {
            get { return _dataTypeInfo.TypeName; }
        }
    }
}