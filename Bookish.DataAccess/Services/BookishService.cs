using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Bookish.DataAccess.Services
{
    public interface IBookishService
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<CatalogueEntry> GetCatalogue(string filter);
        IEnumerable<BookCopy> GetCopies(string filter);
        IEnumerable<BookLoan> GetLoans(string userId);
        void AddBook(string title, string author, string isbn, int totalCopies);
        void LoanBook(int copyId, string lenderId);
        void ReturnBook(int copyId);
        bool DoesIsbnExist(string isbn);
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
                WHERE
                    Books.Title LIKE @Filter OR Books.Author LIKE @Filter
                GROUP BY
                    Books.ISBN, Books.Title, Books.Author
                ORDER BY
	                Books.Title";

            return dbConnection.Query<CatalogueEntry>(query, new { Filter = $"%{filter}%" });
        }

        public IEnumerable<BookCopy> GetCopies(string isbn)
        {
            var query =
                @"SELECT
                    Books.Title AS Title,
                    Books.Author AS Author,
                    BookCopies.CopyID AS CopyID,
                    AspNetUsers.UserName AS Username,
                    Loans.DueDate AS DueDate
                    FROM
                BookCopies
                    LEFT JOIN Books ON Books.ISBN = BookCopies.ISBN
                    LEFT JOIN Loans ON BookCopies.CopyID = Loans.CopyID
                    LEFT JOIN AspNetUsers ON Loans.LenderID = AspNetUsers.Id
                WHERE
                    BookCopies.ISBN = @Filter
                ORDER BY
                    DueDate";

            return dbConnection.Query<BookCopy>(query, new { Filter = isbn });
        }

        public IEnumerable<BookLoan> GetLoans(string userId)
        {
            var query =
                @"SELECT
                    Loans.CopyID AS CopyID,
                    Books.Title AS Title,
                    Books.Author AS Author,
                    Books.ISBN AS ISBN,
                    Loans.DueDate AS DueDate
                FROM
                    Loans
                    JOIN BookCopies ON Loans.CopyID = BookCopies.CopyID
                    JOIN Books ON BookCopies.ISBN = Books.ISBN
                WHERE
                    Loans.LenderID = @UserID
                ORDER BY
	                Loans.DueDate";

            return dbConnection.Query<BookLoan>(query, new { UserID = userId });
        }

        public void AddBook(string title, string author, string isbn, int totalCopies)
        {
            var valuesList = Enumerable.Repeat("(@ISBN)", totalCopies);

            var query =
                @"INSERT INTO Books (ISBN, Title, Author)
                VALUES (@ISBN, @Title, @Author); " +
                @"INSERT INTO BookCopies (ISBN) VALUES " +
                string.Join(", ", valuesList);

            dbConnection.Execute(query, new { Title = title, Author = author, ISBN = isbn });
        }

        public bool DoesIsbnExist(string isbn)
        {
            var query = "SELECT ISBN FROM Books WHERE ISBN = @ISBN";

            return dbConnection.Query<string>(query, new { ISBN = isbn })
                .Any();
        }

        public void LoanBook(int copyId, string lenderId)
        {
            var query = "INSERT INTO Loans (CopyID, LenderID, DueDate) VALUES (@CopyID, @LenderID, @DueDate)";

            var parameters = new
            {
                CopyID = copyId,
                LenderID = lenderId,
                DueDate = DateTime.Now
                    .AddDays(14)
                    .ToString("yyyy-MM-dd")
            };

            dbConnection.Execute(query, parameters);
        }

        public void ReturnBook(int copyId)
        {
            var query = $"DELETE FROM Loans WHERE CopyID = @CopyID";

            dbConnection.Execute(query, new {CopyID = copyId});
        }
    }
}