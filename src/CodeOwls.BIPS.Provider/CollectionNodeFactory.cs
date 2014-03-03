using System;
using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS
{
    public class CollectionNodeFactory<T> : NodeFactoryBase
    {
        private readonly string _collectionName;
        private readonly IEnumerable<T> _items;
        private readonly Converter<T, INodeFactory> _adapter;

        public CollectionNodeFactory(string collectionName, IEnumerable<T> items, Converter<T, INodeFactory> adapter)
        {
            _collectionName = collectionName;
            _items = items;
            _adapter = adapter;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(PowerShell.Provider.PathNodeProcessors.IContext context)
        {
            List<INodeFactory> nodes = null;
            nodes = _items.ToList().ConvertAll(_adapter);
            return nodes;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( new Container(this), Name );
        }

        public override string Name
        {
            get { return _collectionName; }
        }
    }

    public class Container
    {
        private readonly INodeFactory _factory;

        public Container(INodeFactory factory)
        {
            _factory = factory;
        }

        public string Name { get { return _factory.Name; }}
    }
}