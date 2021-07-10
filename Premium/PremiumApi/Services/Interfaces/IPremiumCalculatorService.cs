using PremiumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.Services.Interfaces
{
    public interface IPremiumCalculatorService
    {
        PremiumResponse CalculatePremium(UserDetail userDetail);
    }
}
