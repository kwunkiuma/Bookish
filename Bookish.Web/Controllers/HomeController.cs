using System.Diagnostics;
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

        public IActionResult Catalogue(string filter = "")
        {
            var catalogue = bookishService.GetCatalogue(filter);
            var model = new CatalogueViewModel(catalogue);
            return View(model);
        }

        public IActionResult Copies(string isbn, string title, string author)
        {
            var copies = bookishService.GetCopies(isbn);
            var model = new CopiesViewModel(title, author, copies);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}