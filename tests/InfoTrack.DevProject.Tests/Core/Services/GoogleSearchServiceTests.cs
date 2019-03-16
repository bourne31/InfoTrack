using AutoMoqCore;
using InfoTrack.DevProject.Application.Interfaces;
using InfoTrack.DevProject.Application.Services.Google;
using InfoTrack.DevProject.Domain.Search;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Tests.Core.Services
{
    [TestFixture]
    public class GoogleSearchServiceTests
    {
        private GoogleSearchService _googleSearchService;
        private AutoMoqer _mocker;
        private SearchResult _searchResult;

        private const int Rank = 1;
        private const string Url = "www.infotrack.com.au";
        private const string SearchText = "online title search";

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();

            _searchResult = new SearchResult
            {
                Rank = Rank,
                Url = Url
            };

            _mocker.GetMock<ISearchClient>()
                .Setup(u => u.SearchAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new List<SearchResult> { _searchResult });

            _googleSearchService = _mocker.Create<GoogleSearchService>();
        }

        [Test]
        public async Task TestGetUrlRankingsShouldReturnListOfSearchResult()
        {
            // Act
            var results = await _googleSearchService.GetUrlRankingsAsync(SearchText, Url);

            // Assert
            Assert.That(results[0].Rank, Is.EqualTo(Rank));
            Assert.That(results[0].Url, Is.EqualTo(Url));
        }

        [Test]
        public async Task TestGetUrlRankingsShouldReturnNull()
        {
            // Arrange
            _mocker.GetMock<ISearchClient>()
               .Setup(u => u.SearchAsync(It.IsAny<string>(), It.IsAny<int>()))
               .ReturnsAsync((List<SearchResult>)null);

            // Act
            var results = await _googleSearchService.GetUrlRankingsAsync(SearchText, Url);

            // Assert
            Assert.That(results, Is.EqualTo(null));
        }
    }
}
