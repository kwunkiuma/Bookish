using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using BarcodeLib;

namespace Bookish.DataAccess.Services
{
    public interface IBarcodeService
    {
        NewBook GetNewBookBarcodes(string isbn);
    }

    public class BarcodeService : IBarcodeService
    {
        private readonly IBookishService bookishService;

        public BarcodeService(IBookishService bookishService)
        {
            this.bookishService = bookishService;
        }

        public NewBook GetNewBookBarcodes(string isbn)
        {
            var bookCopies = bookishService.GetCopies(isbn).ToList();
            var title = bookCopies.First().Title;
            var author = bookCopies.First().Author;
            var encodedBarcodes = new List<string>();

            foreach (var copy in bookCopies)
            {
                encodedBarcodes.Add(EncodeBarcode($"{isbn}{copy.CopyId}"));
            }

            return new NewBook(title, author, encodedBarcodes);
        }

        private string EncodeBarcode(string isbn)
        {
            var barcode = new Barcode();
            var image = barcode.Encode(TYPE.CODE11, isbn, Color.Black, Color.White, 400, 180);

            using var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            var imageBytes = stream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }
}