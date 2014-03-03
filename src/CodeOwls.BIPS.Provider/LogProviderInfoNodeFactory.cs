using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class LogProviderInfoNodeFactory : NodeFactoryBase
    {
        private readonly LogProviderInfo _logInfo;

        public LogProviderInfoNodeFactory(LogProviderInfo logInfo)
        {
            _logInfo = logInfo;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode(_logInfo, Name);
        }

        public override string Name
        {
            get { return _logInfo.Name; }
        }
    }
}