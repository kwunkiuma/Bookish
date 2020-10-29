using System.Collections.Generic;
using System.Linq;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CopiesViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public IEnumerable<BookCopy> Copies { get; }

        public CopiesViewModel(IEnumerable<BookCopy> copies)
        {
            Copies = copies;

            var copy = Copies.First();
            Title = copy.Title;
            Author = copy.Author;
        }

        public string GetStatus(BookCopy copy)
        {
            return string.IsNullOrEmpty(copy.Username)
                ? $"Unavailable - held by {copy.Username} and due on {copy.DueDate.ToShortDateString()}"
                : "Available to borrow";
        }
    }
}
