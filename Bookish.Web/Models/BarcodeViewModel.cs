using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class BarcodeViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public IEnumerable<string> BarcodeList { get; }

        public BarcodeViewModel(NewBook newBook)
        {
            Title = newBook.Title;
            Author = newBook.Author;
            BarcodeList = newBook.EncodedBarcodes;
        }
    }
}