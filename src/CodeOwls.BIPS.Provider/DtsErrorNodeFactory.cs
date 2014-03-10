using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class DtsErrorNodeFactory : NodeFactoryBase
    {
        private readonly DtsError _error;

        public DtsErrorNodeFactory(DtsError error)
        {
            _error = error;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode(_error, Name);
        }

        public override string Name
        {
            get { return _error.ErrorCode.ToString(); }
        }
    }
}