using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services.Utility
{
    public class QueryHelper
    {
        public static DataSet ExecSql(string factory, string sql)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[factory].ConnectionString;
            using (var conn = new SqlConnection(connectionString))
            {
                var ds = new DataSet();
                try
                {
                    conn.Open();
                    var command = new SqlDataAdapter(sql, conn);
                    command.Fill(ds);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
                return ds;
            }
        }
    }
}
