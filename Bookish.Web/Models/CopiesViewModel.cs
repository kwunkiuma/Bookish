using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CopiesViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public IEnumerable<CopiesEntry> Copies { get; }

        public CopiesViewModel(string title, string author, IEnumerable<CopiesEntry> copies)
        {
            Title = title;
            Author = author;
            Copies = copies;
        }
    }
}
