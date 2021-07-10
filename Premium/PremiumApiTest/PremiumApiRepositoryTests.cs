using Microsoft.Extensions.Logging;
using Moq;
using PremiumApi.Models;
using PremiumApi.Repository;
using Xunit;

namespace PremiumApiTest
{
    public class PremiumApiRepositoryTests
    {
        [Fact]
        public void Should_Return_CalculatedPremium_When_Input_Is_Valid()
        {
            // Arrange
            var mockLoggerFactory = new Mock<ILogger<PremiumCalculatorRepository>>();
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

            // Act
            var premiumCalculatorRepository = new PremiumCalculatorRepository(mockLoggerFactory.Object);
            var result = premiumCalculatorRepository.CalculatePremium(userDetail);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(premiumResponse.IsSuccess, result.IsSuccess);
        }
    }
}
