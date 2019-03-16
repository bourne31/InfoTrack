using InfoTrack.DevProject.Application.Services;
using InfoTrack.DevProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISearchService _searchService;

        public HomeController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public IActionResult Index()
        {
            var model = new SearchViewModel
            {
                DomainUrl = "www.infotrack.com.au",
                SearchText = "online title search"
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var searchResults = await _searchService.GetUrlRankingsAsync(model.SearchText, model.DomainUrl);

            var rankings = searchResults?.ToArray();

            const string resultTemplate = "Ranking: {0}";
            var ranks = rankings?.Select(r => r.Rank);

            var displayMessage = (rankings != null && rankings.Any())
                ? string.Format(resultTemplate, string.Join(", ", ranks))
                : string.Format(resultTemplate, 0);

            model.Rankings = displayMessage;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
