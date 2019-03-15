using InfoTrack.DevProject.Application.Services;
using InfoTrack.DevProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGoogleSearchService _googleSearchService;

        public HomeController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        public async Task<IActionResult> Index()
        {
            const string searchText = "online title search";
            const string domainUrl = "www.infotrack.com.au";
            var result = await _googleSearchService.GetUrlRankings(searchText, domainUrl);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
