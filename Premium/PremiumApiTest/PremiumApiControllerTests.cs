using Moq;
using PremiumApi.Controllers;
using PremiumApi.Models;
using PremiumApi.Services.Interfaces;
using Xunit;

namespace PremiumApiTest
{
    public class PremiumApiControllerTests
    {
        [Fact]
        public void Should_Return_Premium_When_Input_Is_Valid()
        {
            // Arrange  
            var premiumCalculatorServiceMock = new Mock<IPremiumCalculatorService>();
            var userDetail = new UserDetail
            {
                Age = 20,
                RatingFactor = 1.5M,
                SumInsured = 3000
            };
            var premiumResponse = new PremiumResponse
            {
                IsSuccess = true,
                Premium = "756.0",
                ResponseMessage = "Premuin calculation successfull."
            };
            premiumCalculatorServiceMock.Setup(_ => _.CalculatePremium(userDetail)).Returns(premiumResponse);

            // Act
            var calculatePremiumController = new CalculatePremiumController(premiumCalculatorServiceMock.Object);
            var result = calculatePremiumController.Post(userDetail);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(premiumResponse.IsSuccess, ((PremiumResponse)((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value).IsSuccess);
        }
    }
}
