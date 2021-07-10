using PremiumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.Repository.Interface
{
    public interface IPremiumCalculatorRepository
    {
        PremiumResponse CalculatePremium(UserDetail userDetail);
    }
}
