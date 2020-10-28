using System;
using System.Collections.Generic;
using System.Linq;
using Bookish.DataAccess;
using Microsoft.AspNetCore.Http.Connections;

namespace Bookish.Web.Models
{
    public class CopiesViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public IEnumerable<BookCopy> Copies { get; }

        public CopiesViewModel(IEnumerable<BookCopy> copies)
        {
            Copies = copies.ToList();

            Title = Copies.First().Title;
            Author = Copies.First().Author;
        }

        public string GetStatus(BookCopy copy)
        {
            if (string.IsNullOrEmpty(copy.Username))
            {
                return "Available to borrow";
            }

            return $"Unavailable - held by {copy.Username} and due on {copy.DueDate.ToShortDateString()}";
        }
    }
}
