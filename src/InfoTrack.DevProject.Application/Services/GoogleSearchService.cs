using InfoTrack.DevProject.Application.Interfaces;
using InfoTrack.DevProject.Domain.Google;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Application.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly IGoogleSearchClient _googleSearchClient;

        public GoogleSearchService(IGoogleSearchClient googleSearchClient)
        {
            _googleSearchClient = googleSearchClient;
        }

        public async Task<IEnumerable<GoogleResult>> GetUrlRankings(string searchText, string domainUrl)
        {
            var googleResults = await _googleSearchClient.SearchGoogle(searchText);

            var rankings = googleResults.Where(a => a.Url.Contains(domainUrl.ToLower()));

            return rankings;
        }
    }
}
