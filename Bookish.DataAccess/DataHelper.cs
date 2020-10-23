using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bookish.DataAccess
{
    public class DataHelper
    {
        private static readonly IDbConnection DbConnection = new SqlConnection(@"Server = localhost; Database = Bookish; Trusted_Connection = True;");

        public static IEnumerable<T> ExecuteQuery<T>(string query)
        {
            return DbConnection.Query<T>(query);
        }
    }
}