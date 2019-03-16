using InfoTrack.DevProject.Application.Interfaces;
using InfoTrack.DevProject.Domain.Search;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Application.Services.Google
{
    public class GoogleSearchService : ISearchService
    {
        private readonly ISearchClient _searchClient;

        public GoogleSearchService(ISearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        public async Task<List<SearchResult>> GetUrlRankingsAsync(string searchText, string domainUrl)
        {
            const int pageEnd = 90;
            var googleResults = await _searchClient.SearchAsync(searchText, pageEnd);

            var rankings = googleResults?.Where(a => a.Url.Contains(domainUrl.ToLower()));

            return rankings?.ToList();
        }
    }
}
