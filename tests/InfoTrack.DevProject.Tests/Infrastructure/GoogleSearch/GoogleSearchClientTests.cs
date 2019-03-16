using InfoTrack.DevProject.Infrastructure.GoogleSearch;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace InfoTrack.DevProject.Tests.Infrastructure.GoogleSearch
{
    [TestFixture]
    public class GoogleSearchClientTests
    {

        [Test]
        public async Task TestSearchShouldReturnListOfSearchResult()
        {
            // Arrange
            const string baseAddress = "https://google.com.au/";
            const string searchText = "online title search";
            const int page = 0;
            var expectedUri = new Uri($"{baseAddress}search?q={searchText}&start={page}");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("<html>" +
                   "<h3 class=\"r\">" +
                   "<a href=https://www.infotrack.com.au>InfoTrack</a>" +
                   "</h3>" +
                   "</html>"),
               })
               .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri(baseAddress),
            };

            var googleSearchClient = new GoogleSearchClient(httpClient);

            // Act
            var result = await googleSearchClient.SearchAsync(searchText, page);

            // Assert
            Assert.IsNotNull(result); 
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Rank, Is.EqualTo(1));
            Assert.That(result[0].Url, Is.EqualTo("www.infotrack.com.au"));

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), 
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get &&
                  req.RequestUri == expectedUri 
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
