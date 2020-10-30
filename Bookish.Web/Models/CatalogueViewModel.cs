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

        private const int PageSize = 5;

        public CatalogueViewModel(IEnumerable<CatalogueEntry> catalogue, int page = 1, string filter = "")
        {
            Page = page;
            Filter = filter;

            var fullCatalogue = catalogue.ToList();
            LastPage = (int) Math.Ceiling(fullCatalogue.Count() / (double) PageSize);
            Catalogue = fullCatalogue
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
        }

        public string GetPageNumberClass(int pageNumber)
        {
            return pageNumber == Page
                ? "page-item active"
                : "page-item";
        }
    }
}