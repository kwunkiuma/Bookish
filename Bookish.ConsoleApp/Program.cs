using System;
using Bookish.DataAccess;

namespace Bookish.ConsoleApp
{
    class Program
    {

        public static void ListBooks(BookishService service)
        {
            var response = service.GetBooks();
            Console.WriteLine(string.Join("\n", response));
        }

        static void Main()
        {
            var service = new BookishService();

            ListBooks(service);
        }
    }
}