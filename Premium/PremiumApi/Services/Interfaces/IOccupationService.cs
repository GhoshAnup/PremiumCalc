using PremiumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.Services.Interfaces
{
    public interface IOccupationService
    {
        List<OccupationFactor> GetOccupations();
    }
}
