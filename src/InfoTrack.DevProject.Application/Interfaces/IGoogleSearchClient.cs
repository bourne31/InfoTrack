using InfoTrack.DevProject.Domain.Google;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Application.Interfaces
{
    public interface IGoogleSearchClient
    {
        Task<IEnumerable<GoogleResult>> SearchGoogle(string searchText);
    }
}
