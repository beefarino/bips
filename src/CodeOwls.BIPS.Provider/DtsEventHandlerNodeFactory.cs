using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class DtsEventHandlerNodeFactory : NodeFactoryBase
    {
        private readonly DtsEventHandler _input;

        public DtsEventHandlerNodeFactory(DtsEventHandler input)
        {
            _input=input;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var nodes = new List<INodeFactory>();

            
            var executables = _input.Executables.Cast<Executable>().ToList();
            nodes.Add(new CollectionNodeFactory<Executable>("Executables", executables, c => new ExecutableNodeFactory(c, _input.Executables)));

            var props = _input.Properties.Cast<DtsProperty>().ToList();
            nodes.Add(new CollectionNodeFactory<DtsProperty>("Properties", props, c => new DtsPropertyNodeFactory(c)));

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