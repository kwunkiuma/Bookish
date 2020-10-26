using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bookish.DataAccess
{
    public interface IBookishService
    {
        IEnumerable<Book> GetBooks();
    }

    public class BookishService : IBookishService
    {
        private readonly IDbConnection dbConnection = new SqlConnection("Server = localhost; Database = Bookish; Trusted_Connection = True;");

        public IEnumerable<Book> GetBooks()
        {
            return dbConnection.Query<Book>("SELECT * FROM BOOKS");
        }
    }
}