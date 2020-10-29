using System;
using System.Collections.Generic;
using System.Linq;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class CatalogueViewModel
    {
        public IEnumerable<CatalogueEntry> Catalogue { get; }
        public string Filter { get; }
        public int Page { get; }
        public int LastPage { get; }

        public CatalogueViewModel(IEnumerable<CatalogueEntry> catalogue, int page = 1, string filter = "")
        {
            Page = page;
            Filter = filter;

            Catalogue = catalogue.ToList();
            LastPage = (int)Math.Ceiling(((decimal) Catalogue.Count() / 5));
            Catalogue = Catalogue
                .Skip((page - 1) * 5)
                .Take(5);
        }

        public string GetListClasses(int pageNumber)
        {
            return (pageNumber == Page)
                ? "page-item active"
                : "page-item";
        }
    }
}