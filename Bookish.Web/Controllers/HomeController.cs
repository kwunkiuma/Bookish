using System.Diagnostics;
using System.Linq;
using Bookish.DataAccess;
using Bookish.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IBookishService bookishService;

        public HomeController(ILogger<HomeController> logger, IBookishService bookishService)
        {
            this.logger = logger;
            this.bookishService = bookishService;
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
            try
            {
                bookishService.AddBook(title, author, isbn, totalCopies);
            }
            catch
            {
                return RedirectToAction("NewBook", new { title, author, isbn, totalCopies });
            }

            return RedirectToAction("Barcodes");
        }

        public IActionResult Barcodes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}