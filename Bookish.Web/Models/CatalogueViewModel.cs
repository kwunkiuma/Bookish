using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueViewModel
    {
        public IEnumerable<CatalogueEntry> Catalogue { get; }

        public CatalogueViewModel(IEnumerable<CatalogueEntry> catalogue)
        {
            Catalogue = catalogue;
        }
    }
}