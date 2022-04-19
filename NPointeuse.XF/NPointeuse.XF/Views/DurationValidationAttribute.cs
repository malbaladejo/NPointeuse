using NPointeuse.Infra.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NPointeuse.XF.Views
{
    internal class DurationValidationAttribute : ValidationAttribute
    {
        public override ValidationResult IsValid(object source, object value)
        {
            try
            {
                if (value is string duration)
                    return this.ValidateDuration(duration);

                return ValidationResult.Fail("value must be a string");
            }
            catch (Exception e)
            {
                return ValidationResult.Fail(e.Message);
            }
        }

        private ValidationResult ValidateDuration(string value)
        {
            if (!TimeSpan.TryParse(value, out var timespan))
                return ValidationResult.Fail("Incorrect format. Must be 00:00:00");

            if (timespan.TotalHours > 24)
                return ValidationResult.Fail("Value must be less than 24H.");

            return ValidationResult.Success;
        }
    }
}
