using InfoTrack.DevProject.Application.Services;
using InfoTrack.DevProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using InfoTrack.DevProject.Domain.Google;

namespace InfoTrack.DevProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGoogleSearchService _googleSearchService;

        public HomeController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        public IActionResult Index()
        {
            var model = new GoogleSearchViewModel
            {
                DomainUrl = "www.infotrack.com.au",
                SearchText = "online title search"
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(GoogleSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var googleResults = await _googleSearchService.GetUrlRankings(model.SearchText, model.DomainUrl);

            var rankings = googleResults as GoogleResult[] ?? googleResults.ToArray();

            const string resultTemplate = "Ranking: {0}";
            var ranks = rankings.Select(r => r.Rank);

            ViewData["Ranking"] = rankings.Any()
                ? string.Format(resultTemplate, string.Join(", ", ranks))
                : string.Format(resultTemplate, 0);

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
