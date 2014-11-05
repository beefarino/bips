using System;
using CodeOwls.PowerShell.Provider.PathNodes;

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
}