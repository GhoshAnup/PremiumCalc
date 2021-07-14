using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Premium.Models.ViewModels
{
    public class PremiumViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only.")]
        public string Name { get; set; }

        [Range(16, 100, ErrorMessage = "Age must be between 16 between 100")]
        public string Age { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string FactorRating { get; set; }
        public IEnumerable<OccupationFactor> Occupation { get; set; }

        [Required]
        [DisplayName("Death – Sum Insured")]
        [Range(1000, 10000000, ErrorMessage = "Death – Sum Insured value must be between $1000 and $100,00,000")]
        public int? SumInsured { get; set; }
    }
}
