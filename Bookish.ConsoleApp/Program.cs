﻿using System;
using Bookish.DataAccess.Services;

namespace Bookish.ConsoleApp
{
    class Program
    {
        public static void ListBooks()
        {
            var response = new BookishService().GetBooks();
            Console.WriteLine(string.Join("\n", response));
        }

        static void Main()
        {
            ListBooks();
        }
    }
}