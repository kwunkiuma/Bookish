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
            var fullCatalogue = catalogue.ToList();

            LastPage = (int) Math.Ceiling(fullCatalogue.Count() / (double) PageSize);
            Page = ClampPageNumber(page);
            Catalogue = fullCatalogue
                .Skip((Page - 1) * PageSize)
                .Take(PageSize);

            Filter = filter;
        }

        public string GetPageNumberClass(int pageNumber)
        {
            return pageNumber == Page
                ? "page-item active"
                : "page-item";
        }

        private int ClampPageNumber(int page)
        {
            if (page < 1)
            {
                return 1;
            }
            if (page > LastPage)
            {
                return LastPage;
            }

            return page;
        }
    }
}