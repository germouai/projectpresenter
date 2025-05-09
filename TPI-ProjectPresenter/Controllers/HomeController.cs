using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TPI_ProjectPresenter.Models;
using TPI_ProjectPresenter.Models.ProjectContent;

namespace TPI_ProjectPresenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tst = new ContentItemTextOnly("TextyText from ContentItem");
            var sectiontest = new ContentSection();
            sectiontest.SectionName = "Section Title Test";
            sectiontest.SectionTooltip = "I really hope this works";
            sectiontest.AddContent(tst);

            var imgtest = new ContentItemSingleImage();
            imgtest.ImageRef = "RoadmapTP.png";

            sectiontest.AddContent(imgtest);

            return View(sectiontest);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
