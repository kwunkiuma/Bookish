using System;
using Bookish.DataAccess;

namespace Bookish.ConsoleApp
{
    class Program
    {
        public static void ListBooks()
        {
            var response = DataHelper.ExecuteQuery<Book>(@"SELECT * FROM BOOKS");
            Console.WriteLine(string.Join("\n", response));
        }

        static void Main()
        {
            ListBooks();
        }
    }
}