using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Premium.Models.ViewModels
{
    public class PremiumViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only.")]
        public string Name { get; set; }
        public string Age { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string OccupationType { get; set; }
        public IEnumerable<Occupation> Occupation { get; set; }

        [Required]
        [DisplayName("Death – Sum Insured")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Please enter valid amount")]
        public int? SumInsured { get; set; }
    }
}
