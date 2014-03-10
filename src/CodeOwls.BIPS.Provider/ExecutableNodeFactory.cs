using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class ExecutableNodeFactory : NodeFactoryBase
    {
        private readonly DtsContainer _executable;

        public ExecutableNodeFactory(Executable executable)
        {
            _executable = (DtsContainer)executable;
            
            var host = _executable as TaskHost;
            
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _executable, Name);
        }

        public override string Name
        {
            get { return _executable.Name; }
        }
    }
}