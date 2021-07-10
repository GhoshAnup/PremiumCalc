using FluentValidation;
using PremiumApi.Constants;
using PremiumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.Validation
{
    public class UserDetailValidation :AbstractValidator<UserDetail>
    {
        public UserDetailValidation()
        {
            RuleFor(t => t.Age)
                .GreaterThan(0)
                .WithName("Age")
                .WithMessage(ErrorMessages.InvalidTagWithInvalidValue);

            RuleFor(t => t.RatingFactor)
               .ScalePrecision(2,4)
               .WithName("Age")
               .WithMessage(ErrorMessages.InvalidTagWithInvalidValue);

            RuleFor(t => t.SumInsured)
               .GreaterThan(0)
               .WithName("SumInsured")
               .WithMessage(ErrorMessages.InvalidTagWithInvalidValue);
        }
    }
}
