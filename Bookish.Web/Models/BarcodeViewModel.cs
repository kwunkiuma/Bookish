using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using BarcodeLib;

namespace Bookish.Web.Models
{
    public class BarcodeViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public List<string> BarcodeList { get; }

        public BarcodeViewModel(string title, string author, List<string> barcodeList)
        {
            Title = title;
            Author = author;
            BarcodeList = barcodeList;
        }
    }
}