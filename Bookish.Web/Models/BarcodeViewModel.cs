using System;
using System.Drawing.Imaging;
using System.IO;
using BarcodeLib;

namespace Bookish.Web.Models
{
    public class BarcodeViewModel
    {
        public int TotalCopies { get; }
        public string EncodedBarcode { get; }

        public BarcodeViewModel(string isbn, int totalCopies)
        {
            TotalCopies = totalCopies;

            var b = new Barcode();
            var img = b.Encode(TYPE.CODE11, isbn);

            using var stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            var imageBytes = stream.ToArray();
            EncodedBarcode = Convert.ToBase64String(imageBytes);
        }
    }
}