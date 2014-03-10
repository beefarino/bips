using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace CodeOwls.BIPS
{
    [Cmdlet( VerbsCommon.Get, "DeployedPackage")]
    public class GetDeployedPackageCmdet : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string Path { get; set; }
        protected override void ProcessRecord()
        {
            using (
                var connection =
                    new SqlConnection("Data Source=talon-sql-2012;Initial Catalog=SSISDB;Integrated Security=True"))
            {
                connection.Open();
                using (var cmd = new SqlCommand("execute [ssisdb].[catalog].[get_project] @folder, @project", connection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@folder", "Spikes");
                    cmd.Parameters.AddWithValue("@project", "SpikeSSIS");

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var ordinal = reader.GetOrdinal("project_stream");
                            var pacakgeBits = reader.GetSqlBytes(ordinal);

                            WriteObject( pacakgeBits.Value );
                        }
                    }
                }
            }
        }
    }
}
