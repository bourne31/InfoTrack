using System.Collections.Generic;
using System.Threading.Tasks;
using InfoTrack.DevProject.Domain.Google;

namespace InfoTrack.DevProject.Application.Services
{
    public interface IGoogleSearchService
    {   
        Task<IEnumerable<GoogleResult>> GetUrlRankings(string searchText, string domainUrl);
    }
}