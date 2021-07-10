using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Premium.Helper
{
    public class DateValidator : ValidationAttribute
    {
        int _minimumAge;

        public DateValidator(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {
            bool parsed = DateTime.TryParse((string)value, out var date);
            if (!parsed)
                return false;
            return true;
        }
    }
}
