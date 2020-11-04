using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BarcodeLib;

namespace Bookish.DataAccess.Services
{
    public interface IBarcodeService
    {
        string EncodeBarcode(string isbn);
    }

    public class BarcodeService : IBarcodeService
    {
        public string EncodeBarcode(string isbn)
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