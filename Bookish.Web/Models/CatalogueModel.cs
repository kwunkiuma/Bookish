using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueModel
    {
        public IEnumerable<CatalogueEntry> Catalogue;
        public CatalogueModel(IEnumerable<CatalogueEntry> catalogue)
        {
            Catalogue = catalogue;
        }
    }
}
