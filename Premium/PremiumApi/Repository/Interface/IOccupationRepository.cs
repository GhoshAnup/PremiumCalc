using PremiumApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.Repository.Interface
{
    public interface IOccupationRepository 
    {
        List<OccupationFactor> GetOccupations();
    }
}
