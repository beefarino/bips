using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
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


    public class PackageInfoNodeFactory : NodeFactoryBase
    {
        private readonly PackageInfo _package;
        private readonly PackageInfo _parentPackage;

        public PackageInfoNodeFactory( PackageInfo package ) : this( package,null)
        {
            
        }
        public PackageInfoNodeFactory( PackageInfo package, PackageInfo parentPackage )
        {
            _package = package;
            _parentPackage = parentPackage;
        }

        bool IsFolder
        {
            get { return _package.Flags == DTSPackageInfoFlags.Folder; }
        }

        bool IsPackage
        {
            get { return _package.Flags == DTSPackageInfoFlags.Package; }
        }

        string FolderPath
        {
            get
            {
                var folder = _package.Folder;
                var name = _package.Name;
                var path = folder + "\\" + name;
                return path;
            }
        }
        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {
            if (IsPackage)
            {
                return null;
            }

            var app = BipsRootNodeFactory.Application;
            var folderPath = FolderPath;

            //var packages = app.GetPackageInfos(folderPath, "talon-sql-2012", null, null);
            var packages = app.GetDtsServerPackageInfos(folderPath, "talon-sql-2012");
            var factories = (from PackageInfo package in packages select new PackageInfoNodeFactory(package)).Cast<INodeFactory>().ToList();
            return factories;
        }

        public override IPathNode GetNodeValue()
        {
            var nodeValue = PSObject.AsPSObject(_package);
            nodeValue.Properties.Add( new PSNoteProperty( "IsFolder", IsFolder));
            nodeValue.Properties.Add(new PSNoteProperty("IsPackage", IsPackage));
            nodeValue.Properties.Add(new PSNoteProperty("FolderPath", FolderPath));
            if (IsPackage)
            {
                //var p = BipsRootNodeFactory.Application.LoadFromSqlServer(FolderPath, "talon-sql-2012", null,null, null);
            }
            var node = IsPackage ? (IPathNode) new LeafPathNode(nodeValue, Name) : (IPathNode) new ContainerPathNode(nodeValue, Name);
            return node;
        }

        public override string Name
        {
            get { return _package.Name; }
        }
    }
}