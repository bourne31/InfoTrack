using InfoTrack.DevProject.Application.Interfaces;
using InfoTrack.DevProject.Domain.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Infrastructure.YahooSearch
{
    public class YahooSearchClient : ISearchClient
    {
        public Task<List<SearchResult>> SearchAsync(string searchText, int pageEnd)
        {
            throw new NotImplementedException();
        }
    }
}
