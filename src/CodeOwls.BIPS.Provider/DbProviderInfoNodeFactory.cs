using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class DbProviderInfoNodeFactory : NodeFactoryBase
    {
        private readonly DBProviderInfo _dbInfo;

        public DbProviderInfoNodeFactory(DBProviderInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _dbInfo, Name);
        }

        public override string Name
        {
            get { return _dbInfo.Name; }
        }
    }
}