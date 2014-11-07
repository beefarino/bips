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

            nodes.Add(new ExecutableCollectionNodeFactory("Executables", _input.Executables, c => new ExecutableNodeFactory(c, _input.Executables)));

            nodes.Add(new PrecedenceConstraintCollectionNodeFactory(
                "PrecedenceConstraints", 
                _input.PrecedenceConstraints,
                _input.Executables,
                c => new ObjectNodeFactory<PrecedenceConstraint>(c, ()=>c.Name)));

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