using Microsoft.Extensions.Logging;
using PremiumApi.Models;
using PremiumApi.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.Repository
{
    public class OccupationRepository : IOccupationRepository
    {
        private readonly ILogger<OccupationRepository> _logger;
        public OccupationRepository(ILogger<OccupationRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public List<OccupationFactor> GetOccupations()
        {
            List<Occupation> occupations = new List<Occupation>
            {
                new Occupation { OccupationName = "Cleaner", Rating = "Light Manual" },
                new Occupation { OccupationName = "Doctor", Rating = "Professional" },
                new Occupation { OccupationName = "Author", Rating = "White Collar" },
                new Occupation { OccupationName = "Farmer", Rating = "Heavy Manual" },
                new Occupation { OccupationName = "Mechanic", Rating = "Heavy Manual" },
                new Occupation { OccupationName = "Florist", Rating = "Light Manual" }
            };

            List<OccupationRating> occupationRatings = new List<OccupationRating>
            {
                new OccupationRating { Rating = "Professional", Factor = Convert.ToDecimal(1.0) },
                new OccupationRating { Rating = "White Collar", Factor = Convert.ToDecimal(1.25) },
                new OccupationRating { Rating = "Light Manual", Factor = Convert.ToDecimal(1.50) },
                new OccupationRating { Rating = "Heavy Manual", Factor = Convert.ToDecimal(1.75) }
            };
            List<OccupationFactor> occupationItems = (from o in occupations
                                                       join or in occupationRatings
                                                            on o.Rating equals or.Rating
                                                       select new OccupationFactor()
                                                       {
                                                           OccupationName = o.OccupationName,
                                                           FactorValue = or.Factor.ToString()
                                                       }).ToList();
            return occupationItems;
        }

    }
}
