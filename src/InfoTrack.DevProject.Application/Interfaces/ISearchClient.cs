using InfoTrack.DevProject.Domain.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Application.Interfaces
{
    public interface ISearchClient
    {
        Task<List<SearchResult>> SearchAsync(string searchText, int pageEnd);
    }
}
