using Premium.Models;
using Premium.Models.Response;
using Premium.Models.ViewModels;
using System.Threading.Tasks;

namespace Premium.Services.Interface
{
    public interface IPremiumService
    {
        PremiumViewModel GetOccupationList();
        Task<PremiumResponse> GetPremium(Premiums premium);
    }
}
