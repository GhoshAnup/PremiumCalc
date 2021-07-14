using Moq;
using PremiumApi.Models;
using PremiumApi.Repository.Interface;
using PremiumApi.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace PremiumApiTest
{
    public class PremiumApiServiceTests
    {
        [Fact]
        public void Should_Return_CalculatedPremium_When_Input_Is_Valid()
        {
            // Arrange           
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
            var premiumCalculatorRepository = new Mock<IPremiumCalculatorRepository>();
            premiumCalculatorRepository.Setup(_ => _.CalculatePremium(userDetail)).Returns(premiumResponse);

            // Act
            var premiumService = new PremiumCalculatorService(premiumCalculatorRepository.Object);
            var result = premiumService.CalculatePremium(userDetail);

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(premiumResponse.IsSuccess, result.IsSuccess);
        }

        [Fact]
        public void Should_Return_Occupations_When_Input_Is_Valid()
        {
            // Arrange           
            var ocupationList = new List<OccupationFactor>();           
            var occupationFactor = new OccupationFactor
            {
                FactorValue = "1.5",
                OccupationName = "Doctor"
            };
            ocupationList.Add(occupationFactor);
            var occupationRepository = new Mock<IOccupationRepository>();
            occupationRepository.Setup(_ => _.GetOccupations()).Returns(ocupationList);

            // Act
            var occupationService = new OccupationService(occupationRepository.Object);
            var result = occupationService.GetOccupations();

            // Assert  
            Assert.NotNull(result);
            Assert.Equal(ocupationList.Count, result.Count);
        }
    }
}
