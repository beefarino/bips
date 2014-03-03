using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class DtsNameObjectNodeFactory : NodeFactoryBase
    {
        private readonly IDTSName _connectionInfo;

        public DtsNameObjectNodeFactory(IDTSName connectionInfo)
        {
            _connectionInfo = connectionInfo;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _connectionInfo, Name );
        }

        public override string Name
        {
            get { return _connectionInfo.Name; }
        }
    }
}