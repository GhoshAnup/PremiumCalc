using PremiumApi.Models;
using PremiumApi.Repository.Interface;
using PremiumApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.Services
{
    public class OccupationService : IOccupationService
    {
        private readonly IOccupationRepository occupationRepository;
        public OccupationService(IOccupationRepository occupationRepository)
        {
            this.occupationRepository = occupationRepository;
        }
        public List<OccupationFactor> GetOccupations()
        {
           return occupationRepository.GetOccupations();
        }
    }
}
