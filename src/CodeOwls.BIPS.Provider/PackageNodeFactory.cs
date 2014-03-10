using System.Collections.Generic;
using System.Linq;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class PackageNodeFactory : NodeFactoryBase
    {
        private readonly Package _package;

        public PackageNodeFactory( Package package )
        {
            _package = package;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            var children = new List<INodeFactory>();

            var configurations = _package.Configurations.Cast<Configuration>().ToList();
            children.Add(new CollectionNodeFactory<Configuration>("Configurations", configurations, i => new DtsNameObjectNodeFactory(i)));

            var connections = _package.Connections.Cast<ConnectionManager>().ToList();
            children.Add(new CollectionNodeFactory<ConnectionManager>("Connections", connections,c => new DtsNameObjectNodeFactory(c)));

            var errors = _package.Errors.Cast<DtsError>().OrderBy(e=>e.TimeStamp).ToList();            
            children.Add(new CollectionNodeFactory<DtsError>("Errors",errors, e=>new DtsErrorNodeFactory(e)));

            var executables = _package.Executables.Cast<Executable>().ToList();
            children.Add(new CollectionNodeFactory<Executable>("Executables", executables, c => new ExecutableNodeFactory(c)));

            var exprop = _package.ExtendedProperties.Cast<ExtendedProperty>().ToList();
            children.Add(new CollectionNodeFactory<ExtendedProperty>("ExtendedProperties", exprop, c => new ObjectNodeFactory<ExtendedProperty>(c, ()=>c.Name)));

            var logs = _package.LogProviders.Cast<LogProvider>();
            children.Add( new CollectionNodeFactory<LogProvider>("LogProviders",logs, l=>new ObjectNodeFactory<LogProvider>(l,()=>l.Name)));

            var preconstraints = _package.PrecedenceConstraints.Cast<PrecedenceConstraint>();
            children.Add(new CollectionNodeFactory<PrecedenceConstraint>("PrecedenceConstraints", preconstraints, p => new ObjectNodeFactory<PrecedenceConstraint>(p, () => p.Name)));

            var properties = _package.Properties.Cast<DtsProperty>();
            children.Add(new CollectionNodeFactory<DtsProperty>("Properties", properties, p => new ObjectNodeFactory<DtsProperty>(p, () => p.Name)));

            var warnings = _package.Warnings.Cast<DtsWarning>();
            children.Add(new CollectionNodeFactory<DtsWarning>("Warnings", warnings, p => new ObjectNodeFactory<DtsWarning>(p, () => p.WarningCode.ToString())));

            return children;
        }
        
        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _package,Name);
        }

        public override string Name
        {
            get { return _package.Name; }
        }
    }
}