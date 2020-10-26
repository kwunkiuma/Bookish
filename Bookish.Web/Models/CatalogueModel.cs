using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueModel
    {
        public IEnumerable<Book> Books;

        public CatalogueModel(IEnumerable<Book> books)
        {
            Books = books;
        }
    }
}
