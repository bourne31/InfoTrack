using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.DevProject.Domain.Search;

namespace InfoTrack.DevProject.Application.Services
{
    public interface ISearchService
    {   
        Task<List<SearchResult>> GetUrlRankingsAsync(string searchText, string domainUrl);
    }
}