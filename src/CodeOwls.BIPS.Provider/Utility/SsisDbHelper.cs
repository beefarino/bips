using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOwls.BIPS.Utility
{
    class SsisDbHelper
    {
        private readonly string _serverName;

        public SsisDbHelper( string serverName )
        {
            _serverName = serverName;
        }

        public IEnumerable<string> GetPackagesForProject(SsisDbProjectDescriptor project)
        {
            var connectionString = String.Format(
                    "Data Source={0};Initial Catalog=SSISDB;Integrated Security=True",
                    _serverName
                    );

            var sql = String.Format(@"
select (F.Name + '\\' + P.Name + '\\' + K.Name) as PackagePath
from [ssisdb].[catalog].[packages] as K
inner join [ssisdb].[catalog].[projects] as P on
    P.project_id=K.project_id
inner join [ssisdb].[catalog].[folders] as F on
    P.folder_id=F.folder_id
where K.project_id={0}
order by p.[Name]
",
            project.ProjectId);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var oname = reader.GetOrdinal("PackagePath");
                        
                        while (reader.Read())
                        {
                            yield return reader.GetString( oname );
                        }
                    }
                }
            }
        }

        public IEnumerable<SsisDbProjectDescriptor> GetProjectsForFolder(SsisDbFolderDescriptor folder)
        {
            var connectionString = String.Format(
                    "Data Source={0};Initial Catalog=SSISDB;Integrated Security=True",
                    _serverName
                    );

            var sql = String.Format(@"
select P.*, F.Name as folder_name
from [ssisdb].[catalog].[projects] as P
inner join [ssisdb].[catalog].[folders] as F on
    P.folder_id=F.folder_id
where P.folder_id={0}
order by p.[Name]
", 
            folder.FolderId);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var oname = reader.GetOrdinal("name");
                        var oid = reader.GetOrdinal("project_id");
                        var foid = reader.GetOrdinal("folder_id");
                        var odesc = reader.GetOrdinal("description");
                        var ofolder = reader.GetOrdinal("folder_name");
                        var odeployedby = reader.GetOrdinal("deployed_by_name");
                        var oinstant = reader.GetOrdinal("created_time");
                        var odeployinstant = reader.GetOrdinal("last_deployed_time");
                        var ovalidationinstant = reader.GetOrdinal("last_validation_time");
                        var ovalidationstatus = reader.GetOrdinal("validation_status");

                        while (reader.Read())
                        {
                            var projectId = reader.GetInt64(oid);
                            var folderId = reader.GetInt64(foid);
                            var name = reader.GetString(oname);
                            var folderName = reader.GetString(ofolder);
                            var description = reader.IsDBNull(odesc) ? null : reader.GetString(odesc);
                            var deployedBy = reader.GetString(odeployedby);
                            var createdTime = reader.IsDBNull(oinstant) ? DateTimeOffset.MinValue : reader.GetDateTimeOffset(oinstant);
                            var lastValidationTime = reader.IsDBNull(ovalidationinstant) ? DateTimeOffset.MinValue : reader.GetDateTimeOffset(ovalidationinstant);
                            var lastDeployedTime = reader.IsDBNull(odeployinstant) ? DateTimeOffset.MinValue : reader.GetDateTimeOffset(odeployinstant);
                            var validationStatus = reader.GetString(ovalidationstatus)[0];

                            var descriptor = new SsisDbProjectDescriptor
                                                 {
                                                     ProjectId = projectId,
                                                     FolderId = folderId,
                                                     Name = name,
                                                     FolderName = folderName,
                                                     Description = description,
                                                     DeployedBy= deployedBy,
                                                     CreatedTime = createdTime,
                                                     LastValidationTime = lastValidationTime,
                                                     LastDeployedTime = lastDeployedTime,
                                                     ValidationStatus = validationStatus
                                                 };
                            yield return descriptor;
                        }
                    }
                }
            }
        }
        public IEnumerable<SsisDbFolderDescriptor> Folders
        {
            get
            {
                var connectionString = String.Format(
                    "Data Source={0};Initial Catalog=SSISDB;Integrated Security=True",
                    _serverName
                    );

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("select * from [ssisdb].[catalog].[folders] order by [Name]", connection))
                    {                        
                        cmd.CommandType = CommandType.Text;

                        using (var reader = cmd.ExecuteReader())
                        {
                            var oname = reader.GetOrdinal("name");
                            var oid = reader.GetOrdinal("folder_id");
                            var odesc = reader.GetOrdinal("description");
                            var ocreatedby = reader.GetOrdinal("created_by_name");
                            var oinstant = reader.GetOrdinal("created_time");

                            while (reader.Read())
                            {
                                var descriptor = new SsisDbFolderDescriptor
                                                     {
                                                         FolderId = reader.GetInt64( oid ),
                                                         Name=reader.GetString(oname),
                                                         Description = reader.IsDBNull(odesc) ? null : reader.GetString(odesc),
                                                         CreatedBy = reader.GetString(ocreatedby),
                                                         CreatedTime = reader.GetDateTimeOffset(oinstant)
                                                     };
                                yield return descriptor;
                            }
                        }
                    }
                }

            }
        }

        public byte[] GetProjectArchiveFromServer(string folder, string project)
        {
            var connectionString = String.Format(
                "Data Source={0};Initial Catalog=SSISDB;Integrated Security=True",
                _serverName
                );

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var cmd = new SqlCommand("execute [ssisdb].[catalog].[get_project] @folder, @project", connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@folder", folder);
                    cmd.Parameters.AddWithValue("@project", project);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return null;
                        }

                        var ordinal = reader.GetOrdinal("project_stream");
                        var packageBits = reader.GetSqlBytes(ordinal);

                        return packageBits.Value;
                    }
                }
            }
        }

    }

    public class SsisDbPackageDescriptor
    {
        public long PackageId { get; set; }
        public long ProjectId { get; set; }
        public long FolderId { get; set; }
        public string Name { get; set; }
        public string PackagePath { get; set; }
        public string Description { get; set; }
        public Guid PackageGuid { get; set; }
        public Guid VersionGuid { get; set; }
    }

    public class SsisDbFolderDescriptor
    {
        public long FolderId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
    }

    public class SsisDbProjectDescriptor
    {
        public long ProjectId { get; set; }
        public long FolderId { get; set; }
        public string Name { get; set; }
        public string FolderName { get; set; }
        public string Path
        {
            get { return FolderName + "\\" + Name; }
        }
        public string Description { get; set; }
        public string DeployedBy { get; set; }
        public DateTimeOffset LastDeployedTime { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public char ValidationStatus { get; set; }
        public DateTimeOffset LastValidationTime { get; set; }
    }
}

