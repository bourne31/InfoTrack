using System.Collections;
using InfoTrack.DevProject.Application.Interfaces;
using InfoTrack.DevProject.Domain.Search;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Infrastructure.GoogleSearch
{
    public class GoogleSearchClient : ISearchClient
    {
        private const string RequestUriTemplate = "search?q={0}&start={1}";
        private readonly HttpClient _httpClient;

        public GoogleSearchClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SearchResult>> SearchAsync(string searchText, int pageEnd)
        {
            var googleResult = new List<SearchResult>();
            var page = 0;
            const int pageIncrement = 10;
            var requestUri = string.Format(RequestUriTemplate, 
                searchText,
                page);

            do
            {
                await _httpClient.GetAsync(requestUri)
                    .ContinueWith(async (googleSearchTask) =>
                    {
                        var response = await googleSearchTask;
                        if (response.IsSuccessStatusCode)
                        {
                            var html = await response.Content.ReadAsStringAsync();
                            var result = ParseGoogleResult(html, page);
                            googleResult.AddRange(result);
                        }

                        page += pageIncrement;

                        requestUri = string.Format(RequestUriTemplate,
                            searchText,
                            page);
                    });

            } while (page <= pageEnd);

            return googleResult;
        }

        private IEnumerable<SearchResult> ParseGoogleResult(string result, int pageStart)
        {
            const string htmlPattern = "<h3 class=\"r\".*</h3>";
            var regex = new Regex(htmlPattern);

            var matches = regex.Matches(result);

            var googleResult = MapToGoogleResult(matches, pageStart);

            return googleResult;
        }

        private IEnumerable<SearchResult> MapToGoogleResult(IEnumerable matches,
            int pageStart)
        {
            var googleResults = new List<SearchResult>();
            var rank = pageStart + 1;

            foreach (Match match in matches)
            {
                const string urlPattern = @"[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";
                var regex = new Regex(urlPattern);
                var urlMatches = regex.Matches(match.Value);

                googleResults.Add(new SearchResult
                {
                    Url = urlMatches.Count > 0 ? urlMatches[0].Value : string.Empty,
                    Rank = rank
                });

                rank += 1;
            }

            return googleResults;
        }
    }
}
