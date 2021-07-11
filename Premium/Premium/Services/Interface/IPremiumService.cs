using Premium.Models;
using Premium.Models.Response;
using Premium.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Premium.Services.Interface
{
    public interface IPremiumService
    {
        Task<PremiumViewModel> GetOccupationList();
        Task<PremiumResponse> GetPremium(Premiums premium);
    }
}
