using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class NewBookViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public string Isbn { get; }
        public int TotalCopies { get; }

        public NewBookViewModel(string title, string author, string isbn, int totalCopies)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            TotalCopies = totalCopies;
        }
    }
}