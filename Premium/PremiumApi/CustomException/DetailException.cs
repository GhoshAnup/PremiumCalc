using PremiumApi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumApi.CustomException
{
    public class DetailException : Exception
    {
        public Dictionary<string, List<string>> ValidationMessage { get; private set; }

        public DetailException(string message)
            : base(message)
        {
        }

        public DetailException(Dictionary<string, List<string>> validationMessages)
           : base(ErrorMessages.BaseValidationMessage)
        {
            ValidationMessage = validationMessages;
        }
    }
}
