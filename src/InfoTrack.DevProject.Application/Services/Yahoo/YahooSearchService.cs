using InfoTrack.DevProject.Application.Interfaces;
using InfoTrack.DevProject.Domain.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Application.Services.Yahoo
{
    public class YahooSearchService : ISearchService
    {
        private readonly ISearchClient _searchClient;

        public YahooSearchService(ISearchClient searchClient)
        {
            _searchClient = searchClient;
        }

        public Task<List<SearchResult>> GetUrlRankingsAsync(string searchText, string domainUrl)
        {
            throw new NotImplementedException();
        }
    }
}
