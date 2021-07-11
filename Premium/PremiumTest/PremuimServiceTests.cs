using AutoFixture;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Premium.Models;
using Premium.Models.Response;
using Premium.Services;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PremiumTest
{
    public class PremuimServiceTests
    {
        [Fact]
        public void Should_Return_OccupationList_When_Input_Is_Valid()
        {
            // Arrange  
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var httpClientFactory = new Mock<IHttpClientFactory>();

            // Act
            var premiumService = new PremiumService(configuration, httpClientFactory.Object);
            var result = premiumService.GetOccupationList();

            // Assert  
            Assert.NotNull(result);
           // Assert.True(result.Occupation.Count() > 0);
        }

        [Fact]
        public void Should_GetPremium_When_Input_Is_Valid()
        {
            // Arrange  
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var fixture = new Fixture();

            var PremiumResponse = new PremiumResponse
            {
                IsSuccess =true,
                Premium= "756.0",
                ResponseMessage= "Premuin calculation successfull."
            };
            var json = JsonConvert.SerializeObject(PremiumResponse);

            mockHttpMessageHandler.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(PremiumResponse), Encoding.UTF8, "application/json")
               }).Verifiable();       

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = fixture.Create<Uri>()
            };
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var premium = new Premiums
            {
                Age="20",
                FactorRating = "1.5",
                SumInsured = 3000
            };

            // Act
            var premiumService = new PremiumService(configuration, httpClientFactory.Object);
            var result = premiumService.GetPremium(premium);

            //Assert
            Assert.Equal(PremiumResponse.IsSuccess, result.Result.IsSuccess);
            httpClientFactory.Verify(f => f.CreateClient(It.IsAny<String>()), Times.Once);
        }

        [Fact]
        public void Should_Not_GetPremium_When_Input_Is_Not_Valid()
        {
            // Arrange  
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var fixture = new Fixture();

            var PremiumResponse = new PremiumResponse
            {
                IsSuccess = true,
                Premium = "756.0",
                ResponseMessage = "Premuin calculation successfull."
            };
            var json = JsonConvert.SerializeObject(PremiumResponse);

            mockHttpMessageHandler.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK
               }).Verifiable();

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = fixture.Create<Uri>()
            };
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var premium = new Premiums
            {
                Age = "20",
                FactorRating = "1.5",
                SumInsured = 3000
            };

            // Act
            var premiumService = new PremiumService(configuration, httpClientFactory.Object);
            var result = premiumService.GetPremium(premium);

            //Assert
            Assert.NotEqual(PremiumResponse.IsSuccess, result.Result.IsSuccess);
            httpClientFactory.Verify(f => f.CreateClient(It.IsAny<String>()), Times.Once);
        }

    }    
}
