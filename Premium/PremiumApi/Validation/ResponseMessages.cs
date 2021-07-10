using PremiumApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PremiumApi.Validation
{
    public class ResponseMessages
    {
        public static bool HasErrors
        {
            get { return Errors.Any(); }
        }
        public static Dictionary<string, List<string>> Messages { get; set; } = new Dictionary<string, List<string>>();
        private static List<string> Errors { get; set; } = new List<string>();
        public static void Init()
        {
            Errors = new List<string>();
            Messages = new Dictionary<string, List<string>>();
        }
        public static void AddError(string msg, bool throwInterruption = false, Exception ex = null) =>
             Add(msg, ValidationType.Error, throwInterruption, ex);

        public static void Add(string msg, ValidationType type, bool throwInterruption = false, Exception ex = null)
        {
            if (ex != null)
                msg = $"{msg} [{ex.GetBaseException().Message}]";
            switch (type)
            {
                case ValidationType.Error:
                    Errors.Add(msg);
                    break;
            }

            if (throwInterruption)
                throw new Exception(msg, ex);
        }

        public static void AddErrorRange(List<string> msgs) =>
           AddRange(msgs, ValidationType.Error);

        public static void AddRange(List<string> msgs, ValidationType type)
        {
            switch (type)
            {
                case ValidationType.Error:
                    Errors.AddRange(msgs);
                    break;
            }
        }
        public static Dictionary<string, List<string>> Get()
        {
            if (Errors != null && Errors.Any())
                Messages.Add(ValidationType.Error.ToString(), Errors);
            return Messages;
        }
    }
}
