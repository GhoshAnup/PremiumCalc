using Microsoft.Extensions.Logging;
using PremiumApi.Constants;
using PremiumApi.Models;
using PremiumApi.Repository.Interface;
using System;

namespace PremiumApi.Repository
{
    public class PremiumCalculatorRepository : IPremiumCalculatorRepository
    {
        private readonly ILogger<PremiumCalculatorRepository> _logger;
        public PremiumCalculatorRepository(ILogger<PremiumCalculatorRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public PremiumResponse CalculatePremium(UserDetail userDetail)
        {
            var response = new PremiumResponse();
            try
            {
                var premium = (userDetail.SumInsured * userDetail.RatingFactor * userDetail.Age) / 1000 * 12;               
                response.Premium = Math.Round(premium, 2).ToString();
                response.ResponseMessage = ErrorMessages.SucessMessage;
                response.IsSuccess = true;
            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.ResponseMessage = ErrorMessages.FaliureMessage;
                _logger.LogError($"{exception.Message}");
            }
            return response;
        }
    }
}
