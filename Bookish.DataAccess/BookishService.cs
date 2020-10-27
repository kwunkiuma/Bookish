using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Bookish.DataAccess
{
    public interface IBookishService
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<CatalogueEntry> GetCatalogue(string filter);
        IEnumerable<CopiesEntry> GetCopies(string filter);
    }

    public class BookishService : IBookishService
    {
        private readonly IDbConnection dbConnection = new SqlConnection("Server = localhost; Database = Bookish; Trusted_Connection = True;");

        public IEnumerable<Book> GetBooks()
        {
            return dbConnection.Query<Book>("SELECT * FROM BOOKS");
        }

        public IEnumerable<CatalogueEntry> GetCatalogue(string filter)
        {
            var query =
                @"SELECT
	                Books.Title AS Title,
	                Books.Author AS Author,
	                Books.ISBN AS ISBN,
	                COUNT(BookCopies.CopyID) AS Copies,
	                COUNT(BookCopies.CopyID) - COUNT(Loans.CopyID) AS Available
                FROM 
	                Books
	                LEFT JOIN BookCopies ON BookCopies.ISBN = Books.ISBN
	                LEFT JOIN Loans ON Loans.CopyID = BookCopies.CopyID
                WHERE Books.Title LIKE @Filter OR Books.Author LIKE @Filter
                GROUP BY
                    Books.ISBN, Books.Title, Books.Author";

            return dbConnection.Query<CatalogueEntry>(query, new { Filter = $"%{filter}%" });
        }

        public IEnumerable<CopiesEntry> GetCopies(string filter)
        {
            var query =
                @"SELECT
                    BookCopies.CopyID AS CopyID,
                    AspNetUsers.UserName AS Username,
                    Loans.DueDate AS DueDate
                FROM
                    BookCopies
                    LEFT JOIN Loans ON BookCopies.CopyID = Loans.CopyID
                    LEFT JOIN AspNetUsers ON Loans.LenderID = AspNetUsers.Id
                WHERE
                   ISBN = @Filter
                ORDER BY
                    DueDate";

            return dbConnection.Query<CopiesEntry>(query, new { Filter = filter });
        }
    }
}