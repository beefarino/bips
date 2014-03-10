using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class PackageConfigurationNodeFactory : NodeFactoryBase
    {
        private readonly Configuration _configuration;

        public PackageConfigurationNodeFactory(Configuration configuration)
        {
            _configuration = configuration;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _configuration, Name );
        }

        public override string Name
        {
            get { return _configuration.Name; }
        }
    }
}