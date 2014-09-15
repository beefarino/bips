using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CodeOwls.BIPS.Utility
{
    class ServerPackageProxy
    {
        private readonly string _serverName;
        
        class ProjectPath
        {
            public string Folder { get; set; }
            public string Project { get; set; }
        }

        public ServerPackageProxy(string serverName)
        {
            _serverName = serverName;            
            Packages= new List<PackageDescriptor>();
        }

        public List<PackageDescriptor> Packages
        {
            get;
            private set;
        }

        public string[] GetLocalPackageFilePathsForProject(string packagePath)
        {
            var projectPath = GetProjectPath(packagePath);

            if (! ProjectExistsInCache(projectPath))
            {
                var helper = new SsisDbHelper(_serverName);

                var archive = helper.GetProjectArchiveFromServer(projectPath.Folder, projectPath.Project);
                ExpandProjectArchiveToLocalCache(packagePath, archive);
            }
            
            var cachePath = GetProjectCachePath(projectPath);
            return Directory.GetFiles(cachePath, "*.dtsx");
        }

        string CacheRoot
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    ApplicationName,
                    _serverName
                    );

            }
        }

        protected string ApplicationName
        {
            get { return "CodeOwls\\BIPS"; }
        }


        bool ProjectExistsInCache(ProjectPath projectPath)
        {
            var cachePath = GetProjectCachePath(projectPath);
            return Directory.Exists(cachePath);
        }

        public string ExpandProjectArchiveToLocalCache(string packagePath, byte[] archive)
        {
            var tempArchive = Path.GetTempFileName();
            File.WriteAllBytes(tempArchive, archive);
            
            var projectPath = GetProjectPath(packagePath);
            var cachePath = GetProjectCachePath(projectPath);
            
            System.IO.Compression.ZipFile.ExtractToDirectory(tempArchive, cachePath);
            
            return cachePath;
        }

        private string GetProjectCachePath(ProjectPath projectPath)
        {
            var cachePath = Path.Combine(CacheRoot, projectPath.Folder, projectPath.Project);
            return cachePath;
        }


        private static ProjectPath GetProjectPath(string packagePath)
        {
            var re = new Regex(@"(.*)\\([^\\]+)");
            var matches = re.Match(packagePath);
            if (! matches.Success)
            {
                throw new ArgumentException("The specified package path was not an expected format: [" + packagePath + "]",
                    "packagePath");
            }

            var path = new ProjectPath {Folder = matches.Groups[1].Value, Project = matches.Groups[2].Value};
            return path;
        }

        public void Clear()
        {
            Packages = new List<PackageDescriptor>();
            var root = CacheRoot;
            if (Directory.Exists(root))
            {
                Directory.Delete(root, true);
            }
        }
    }
}