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
                return StatusCode(404);
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
            var newBook = barcodeService.GetNewBookBarcodes(isbn);

            if (newBook == null)
            {
                return StatusCode(404);
            }

            var model = new BarcodeViewModel(newBook);
            return View(model);
        }

        [HttpPost]
        public IActionResult LoanBook(int copyId)
        {
            var lenderID = User.Claims.First().Value;

            if (!bookishService.IsCopyAvailable(copyId))
            {
                return StatusCode(403);
            }

            bookishService.LoanBook(copyId, lenderID);

            return RedirectToAction("Loans");
        }

        [HttpPost]
        public IActionResult ReturnBook(int copyId)
        {
            if (GetCurrentUserId() != bookishService.GetCopyLender(copyId))
            {
                return StatusCode(403);
            }

            bookishService.ReturnBook(copyId);
            return RedirectToAction("Loans");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/StatusCode/{errorCode}")]
        public IActionResult Error(int errorCode)
        {
            Response.StatusCode = errorCode;
            return View(new ErrorViewModel(errorCode));
        }

        private string GetCurrentUserId()
        {
            return User.Claims
                .Single(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Value;
        }
    }
}