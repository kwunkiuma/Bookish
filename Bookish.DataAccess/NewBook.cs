using System;
using System.Collections.Generic;

namespace Bookish.DataAccess
{
    public class NewBook
    {
        public string Title { get; }
        public string Author { get; }
        public IEnumerable<string> EncodedBarcodes { get; }

        public NewBook(string title, string author, IEnumerable<string> encodedBarcodes)
        {
            Title = title;
            Author = author;
            EncodedBarcodes = encodedBarcodes;
        }
    }
}