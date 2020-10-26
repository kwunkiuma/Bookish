using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueModels
    {
        public IEnumerable<Book> Books = new BookishService().GetBooks();
    }
}