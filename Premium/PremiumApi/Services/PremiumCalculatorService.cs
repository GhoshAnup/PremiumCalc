using Newtonsoft.Json;
using PremiumApi.Constants;
using PremiumApi.CustomException;
using PremiumApi.Enums;
using PremiumApi.Models;
using PremiumApi.Repository.Interface;
using PremiumApi.Services.Interfaces;
using PremiumApi.Validation;
using System;
using System.Linq;

namespace PremiumApi.Services
{
    public class PremiumCalculatorService: IPremiumCalculatorService
    {
        private readonly IPremiumCalculatorRepository premiumCalculatorRepository;
        public PremiumCalculatorService(IPremiumCalculatorRepository premiumCalculatorRepository)
        {
            this.premiumCalculatorRepository = premiumCalculatorRepository;
        }
        public PremiumResponse CalculatePremium(UserDetail userDetail)
        {
            var premiumResponse = new PremiumResponse();
            try
            {
                ValidateUserDetail(userDetail);
                premiumResponse = premiumCalculatorRepository.CalculatePremium(userDetail);
            }
            catch (DetailException detailException)
            {
                if (detailException.ValidationMessage != null)
                    premiumResponse.ResponseMessage = JsonConvert.SerializeObject(detailException.ValidationMessage, Formatting.Indented).Replace(Environment.NewLine, " ");
            }
            catch(Exception)
            {
                premiumResponse.ResponseMessage = ErrorMessages.FaliureMessage;
            }
            return premiumResponse;
        }

        private void ValidateUserDetail(UserDetail userDetail)
        {
            ResponseMessages.Init();
            var userDetailValidationResult = new UserDetailValidation().Validate(userDetail);
            if (!userDetailValidationResult.IsValid)
            {
                ResponseMessages.AddErrorRange(userDetailValidationResult.Errors.Where(error => error.CustomState != null && error.CustomState.ToString().Equals(ValidationType.Error.ToString()))
                      .Select(error => error.ErrorMessage).ToList());
            }
            if(ResponseMessages.HasErrors)
            throw new DetailException(ResponseMessages.Get()); 
        }
    }
}
