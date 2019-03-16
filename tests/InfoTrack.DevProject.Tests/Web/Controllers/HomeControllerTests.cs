using AutoMoqCore;
using InfoTrack.DevProject.Application.Services;
using InfoTrack.DevProject.Domain.Search;
using InfoTrack.DevProject.Web.Controllers;
using InfoTrack.DevProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Tests.Web.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private AutoMoqer _mocker;
        private SearchResult _searchResult;
        private SearchViewModel _searchViewModel;

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

            _searchViewModel = new SearchViewModel
            {
                DomainUrl = Url,
                SearchText = SearchText
            };

            _mocker.GetMock<ISearchService>()
                .Setup(u => u.GetUrlRankingsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<SearchResult> { _searchResult });

            _controller = _mocker.Create<HomeController>();
        }

        [Test]
        public async Task TestIndexShouldReturnRankings()
        {
            var result = await _controller.Index(_searchViewModel) as ViewResult;
            var model = result.Model as SearchViewModel;

            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.That(model.Rankings, Is.EqualTo("Ranking: 1"));
        }

        [Test]
        public async Task TestIndexShouldReturnNoRankings()
        {
            _mocker.GetMock<ISearchService>()
                .Setup(u => u.GetUrlRankingsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((List<SearchResult>)null);

            var result = await _controller.Index(_searchViewModel) as ViewResult;
            var model = result.Model as SearchViewModel;

            Assert.That(model.Rankings, Is.EqualTo("Ranking: 0"));
        }
    }
}
