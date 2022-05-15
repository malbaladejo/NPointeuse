using NPointeuse.Infra.Client;
using System;
using System.Text.RegularExpressions;

namespace NPointeuse.XF.Views
{
    internal class DateValidationAttribute : ValidationAttribute
    {
        private Regex DateRegex = new Regex("[0-9]{2}\\/[0-9]{2}\\/[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}");

        public override ValidationResult IsValid(object source, object value)
        {
            try
            {
                if (!this.DateRegex.IsMatch(value as string))
                {
                    return ValidationResult.Fail("Incorrect format. Must be dd/mm/yyyy hh:mm");
                }

                if (!DateTime.TryParse(value as string, out var date))
                {
                    return ValidationResult.Fail("Incorrect format. Must be dd/mm/yyyy hh:mm");
                }

                return ValidationResult.Success;
            }
            catch (Exception e)
            {
                return ValidationResult.Fail(e.Message);
            }
        }
    }
}
