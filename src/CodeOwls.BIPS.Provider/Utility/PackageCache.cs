using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dts.Runtime;

namespace CodeOwls.BIPS.Utility
{
    class PackageCache
    {
        private readonly string _cacheName;

        public PackageCache( string cacheName )
        {
            _cacheName = cacheName;
        }

        public Package LoadPackage(string packagePath)
        {
            return null;


        }

        public string CacheRoot
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    ApplicationName,
                    _cacheName
                    );

            }
        }

        protected string ApplicationName
        {
            get { return "BIPS"; }
        }

        public string ExpandProjectArchiveToLocalCache(string packagePath, byte[] archive)
        {
            var tempArchive = Path.GetTempFileName();
            File.WriteAllBytes( tempArchive, archive );
            var projectPath = GetProjectPath(packagePath);
            var cachePath = Path.Combine(CacheRoot, projectPath.Folder);
            System.IO.Compression.ZipFile.ExtractToDirectory( tempArchive, cachePath );
            return cachePath;
        }

        class ProjectPath
        {
            public string Folder { get; set; }
            public string Project { get; set; }
        }

        public byte[] GetProjectArchiveFromServer(string packagePath)
        {
            var connectionString = String.Format(
                "Data Source={0};Initial Catalog=SSISDB;Integrated Security=True",
                _cacheName
                );

            var path = GetProjectPath(packagePath);

            using (var connection =new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("execute [ssisdb].[catalog].[get_project] @folder, @project", connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@folder", path.Folder);
                    cmd.Parameters.AddWithValue("@project", path.Project);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var ordinal = reader.GetOrdinal("project_stream");
                            var packageBits = reader.GetSqlBytes(ordinal);

                            return packageBits.Value;
                        }
                    }
                }
            }
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
    }
}
