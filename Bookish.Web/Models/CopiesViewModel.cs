using System.Collections.Generic;
using System.Linq;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CopiesViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public IEnumerable<CopiesEntry> Copies { get; }

        public CopiesViewModel(IEnumerable<CopiesEntry> copies)
        {
            Copies = copies.ToList();

            Title = Copies.First().Title;
            Author = Copies.First().Author;
        }
    }
}
