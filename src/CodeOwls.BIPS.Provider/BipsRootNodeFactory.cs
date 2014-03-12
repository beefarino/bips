using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using CodeOwls.BIPS.Utility;
using CodeOwls.PowerShell.Provider.PathNodeProcessors;
using CodeOwls.PowerShell.Provider.PathNodes;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS
{
    public class BipsRootNodeFactory : NodeFactoryBase
    {
        private readonly BipsDrive _drive;
        private static readonly Application _application = new Application();

        public BipsRootNodeFactory(BipsDrive drive)
        {
            _drive = drive;
        }

        public override IEnumerable<INodeFactory> GetNodeChildren(IContext context)
        {               
            var nodes = new List<INodeFactory>();
            var serverName = _drive.Server;
            var application = BipsDrive.Application; 

            var connectionInfos = application.ConnectionInfos.Cast<ConnectionInfo>().ToList();
            nodes.Add( new CollectionNodeFactory<ConnectionInfo>( "Connections", connectionInfos, a=>new DtsNameObjectNodeFactory(a) ));

            var typeInfos = application.DataTypeInfos.Cast<DataTypeInfo>().ToList();
            nodes.Add(new CollectionNodeFactory<DataTypeInfo>("DataTypes", typeInfos, a => new DataTypeInfoObjectNodeFactory(a)));

            var dbInfos = application.DBProviderInfos.Cast<DBProviderInfo>().ToList();
            nodes.Add(new CollectionNodeFactory<DBProviderInfo>("DbProviders", dbInfos, a => new DbProviderInfoNodeFactory(a)));

            var logInfos = application.LogProviderInfos.Cast<LogProviderInfo>().ToList();
            nodes.Add(new CollectionNodeFactory<LogProviderInfo>("LogProviders", logInfos, a=>new LogProviderInfoNodeFactory(a)));

            var packages = _drive.PackageCache.Packages;
            if( ! packages.Any() )
            {
                var progress = new ProgressRecord(1, "Loading Packages", "Loading DTS Packages");
                context.WriteProgress( progress );
                var p = LoadDtsPackages(context, progress, application, serverName, string.Empty);
                packages.AddRange(p);
                
                progress.PercentComplete = 33;
                progress.StatusDescription = "Loading SQL Packages";
                context.WriteProgress(progress);
                
                p = LoadSqlPackages(context, progress, application, serverName, string.Empty);
                packages.AddRange(p);

                progress.PercentComplete = 66;
                progress.StatusDescription = "Loading SSISDB Catalog Packages";
                context.WriteProgress(progress); 
                
                p = LoadCatalogPackages(context, progress);
                packages.AddRange(p);                 
                packages.Sort( (q,w)=>StringComparer.InvariantCultureIgnoreCase.Compare( q.Name, w.Name) );

                progress.PercentComplete = 100;
                progress.RecordType = ProgressRecordType.Completed;
                progress.StatusDescription = "Done!";
                context.WriteProgress(progress);
            }

            nodes.Add(new CollectionNodeFactory<PackageDescriptor>("Packages", packages, a => new PackageNodeFactory(a)));
            return nodes;
        }

        private IEnumerable<PackageDescriptor> LoadCatalogPackages(IContext context, ProgressRecord progress)
        {
            progress.CurrentOperation = "Loading folders ...";
            context.WriteProgress(progress);

            var folders = _drive.SsisDbHelper.Folders;
            var projects = folders.ToList().ConvertAll(f => _drive.SsisDbHelper.GetProjectsForFolder(f));
            var packages = new List<PackageDescriptor>();
            foreach (var list in projects)
            {
                foreach (var item in list)
                {
                    progress.CurrentOperation = "Loading packages in project "+ item.Name +"...";
                    context.WriteProgress(progress);
                    var paths = _drive.PackageCache.GetLocalPackageFilePathsForProject(item.Path);
                    foreach (var path in paths)
                    {                        
                        var package = _application.LoadPackage(path, null);
                        packages.Add( new PackageDescriptor( package, item.Path ));
                    }
                }                
            }

            return packages;
        }

        private static IEnumerable<PackageDescriptor> LoadDtsPackages(IContext context, ProgressRecord progressRecord, Application application, string serverName, string path)
        {
            progressRecord.CurrentOperation = "Processing DTS path "+ path + "...";
            context.WriteProgress(progressRecord);

            var packageInfos = application.GetDtsServerPackageInfos(path, serverName);
            var packageItems = packageInfos.Cast<PackageInfo>();
            var folders = from p in packageItems
                          where p.Flags == DTSPackageInfoFlags.Folder
                          select p;
            packageItems = from p in packageItems
                          where p.Flags == DTSPackageInfoFlags.Package
                          select p;
            var packages = packageItems.ToList()
                .ConvertAll( p=> application.LoadFromDtsServer( p.Folder + "\\" + p.Name, serverName, null ) )
                .ConvertAll( p=> new PackageDescriptor( p, path ) )
                .ToList();

            if (folders.Any())
            {
                var children = folders.ToList()
                                      .ConvertAll( f => LoadDtsPackages(context, progressRecord, application, serverName, f.Folder + "\\" + f.Name));
                    

                children.ForEach( packages.AddRange );
            }

            return packages;
        }

        private static IEnumerable<PackageDescriptor> LoadSqlPackages(IContext context, ProgressRecord progressRecord, Application application, string serverName, string path)
        {
            progressRecord.CurrentOperation = "Processing SQL path " + path + "...";
            context.WriteProgress(progressRecord);

            var packageInfos = application.GetPackageInfos(path, serverName, null, null);
            var packageItems = packageInfos.Cast<PackageInfo>();
            var folders = from p in packageItems
                          where p.Flags == DTSPackageInfoFlags.Folder
                          select p;
            packageItems = from p in packageItems
                           where p.Flags == DTSPackageInfoFlags.Package
                           select p;
            var packages = packageItems.ToList()
                                       .ConvertAll(
                                           p =>application.LoadFromSqlServer(p.Folder + "\\" + p.Name, serverName, null, null, null)
                                       )
                                       .ConvertAll(p => new PackageDescriptor(p, path))
                                       .ToList();

            if (folders.Any())
            {
                var children = folders.ToList()
                    .ConvertAll(f => LoadSqlPackages(context, progressRecord, application, serverName, f.Folder + "\\" + f.Name));

                children.ForEach(packages.AddRange);
            }

            return packages;
        }

        public override IPathNode GetNodeValue()
        {
            return new ContainerPathNode( _drive, Name );
        }

        public override string Name
        {
            get { return _drive.Server; }
        }
    }
}