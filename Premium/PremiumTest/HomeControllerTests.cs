using AutoMapper;
using Moq;
using Premium.Controllers;
using Premium.Models;
using Premium.Models.Response;
using Premium.Models.ViewModels;
using Premium.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PremiumTest
{
   public class HomeControllerTests
    {
        private static IMapper _mapper;
        public HomeControllerTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new Premium.ObjectMapper());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        [Fact]
        public void Should_Return_View_When_Input_Is_Valid()
        {
            // Arrange  
        
            var premiumService = new Mock<IPremiumService>();
            var mapper = new Mock<IMapper>();

            // Act
            var homeController = new HomeController(premiumService.Object, mapper.Object);
            var result = homeController.Index();

            // Assert  
            Assert.NotNull(result);
        }

        [Fact]
        public void Should_Calculate_Premium_When_Model_Is_Valid()
        {
            // Arrange  
            var premiumServiceMock = new Mock<IPremiumService>();
            var mapperMock = new Mock<IMapper>();
            var premiumViewModel = new PremiumViewModel{
                Name="TestUser",
                Age="30",
                DateOfBirth=Convert.ToDateTime("01/03/1988"),
                SumInsured =30000,
                FactorRating ="1.5"
            };
            var premiumResponse = new PremiumResponse
            {
                IsSuccess = true,
                Premium = "756.0",
                ResponseMessage = "Premuin calculation successfull."
            };
            var premiums = new Premiums
            {
                Name = "TestUser",
                Age="30",
                SumInsured = 30000,
                FactorRating = "1.5"
            };
            premiumServiceMock.Setup(_ => _.GetPremium(premiums)).Returns(Task.FromResult(premiumResponse));

            // Act
            var homeController = new HomeController(premiumServiceMock.Object, _mapper);
            var result = homeController.Calculate(premiumViewModel);

            // Assert  
            Assert.NotNull(result);
        }

    }
}
