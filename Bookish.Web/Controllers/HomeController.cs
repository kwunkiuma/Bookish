using System.Diagnostics;
using System.Linq;
using Bookish.DataAccess.Services;
using Bookish.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IBookishService bookishService;
        private readonly IBarcodeService barcodeService;

        public HomeController(ILogger<HomeController> logger, IBookishService bookishService, IBarcodeService barcodeService)
        {
            this.logger = logger;
            this.bookishService = bookishService;
            this.barcodeService = barcodeService;
        }

        public IActionResult Catalogue(int page = 1, string filter = "")
        {
            var catalogue = bookishService.GetCatalogue(filter);
            var model = new CatalogueViewModel(catalogue, page, filter);
            return View(model);
        }

        public IActionResult Loans()
        {
            var userId = User.Claims.First().Value;
            var loans = bookishService.GetLoans(userId);
            var model = new LoansViewModel(loans);
            return View(model);
        }

        [Route("/Home/Copies/{isbn}")]
        public IActionResult Copies(string isbn)
        {
            var copies = bookishService.GetCopies(isbn).ToList();

            if (!copies.Any())
            {
                return RedirectToAction("Error", "Home");
            }

            var model = new CopiesViewModel(copies);
            return View(model);
        }

        public IActionResult NewBook(string title = "", string author = "", string isbn = "", int totalCopies = 1)
        {
            var isbnExists = bookishService.DoesIsbnExist(isbn);

            var model = new NewBookViewModel(title, author, isbn, totalCopies, isbnExists);
            return View(model);
        }

        [HttpPost]
        [ActionName("NewBook")]
        public IActionResult NewBookPost(string title, string author, string isbn, int totalCopies)
        {
            if (totalCopies < 1 || bookishService.DoesIsbnExist(isbn))
            {
                return RedirectToAction("NewBook", new { title, author, isbn, totalCopies });
            }

            bookishService.AddBook(title, author, isbn, totalCopies);
            return RedirectToAction("Barcodes", new { isbn });
        }

        [Route("/Home/Barcodes/{isbn}")]
        public IActionResult Barcodes(string isbn)
        {
            var model = new BarcodeViewModel(barcodeService.GetNewBookBarcodes(isbn));
            return View(model);
        }

        [HttpPost]
        public IActionResult LoanBook(int copyId)
        {
            var lenderID = User.Claims.First().Value;
            bookishService.LoanBook(copyId, lenderID);

            return RedirectToAction("Loans");
        }

        [HttpPost]
        public IActionResult ReturnBook(int copyId)
        {
            bookishService.ReturnBook(copyId);
            return RedirectToAction("Loans");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}