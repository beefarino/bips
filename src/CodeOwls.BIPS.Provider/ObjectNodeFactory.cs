using System;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class ObjectNodeFactory<T> : NodeFactoryBase
    {
        private readonly T _nodeItem;
        private readonly Func<string> _nameGetter;

        public ObjectNodeFactory(T nodeItem, Func<string> nameGetter)
        {
            _nodeItem = nodeItem;
            _nameGetter = nameGetter;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode(_nodeItem, Name);
        }

        public override string Name
        {
            get { return _nameGetter(); }
        }
    }

    public class ExtendedPropertyNodeFactory : NodeFactoryBase, IRemoveItem
    {
        private readonly ExtendedProperty _property;
        private readonly ExtendedProperties _collection;

        public ExtendedPropertyNodeFactory( ExtendedProperty property, ExtendedProperties collection )
        {
            _property = property;
            _collection = collection;
        }

        public override IPathNode GetNodeValue()
        {
            return new LeafPathNode( _property, Name );
        }

        public override string Name
        {
            get { return _property.Name; }
        }

        public object RemoveItemParameters { get; private set; }
        public void RemoveItem(IContext context, string path, bool recurse)
        {
            _collection.Remove( _property.ID );
        }
    }
}