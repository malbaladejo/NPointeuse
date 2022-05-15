using NPointeuse.Infra.Client;
using System;

namespace NPointeuse.XF.Views
{
    internal class DateMinValidationAttribute : DateRangeValidationAttribute
    {
        private readonly string dateMinPropertyName;

        public DateMinValidationAttribute(string dateMinPropertyName)
        {
            this.dateMinPropertyName = dateMinPropertyName;
        }

        public override ValidationResult IsValid(object source, object value)
        {
            try
            {
                var date = this.GetDate(value as string);
                if (!date.HasValue)
                    return ValidationResult.Success;

                var minDate = this.GetDateFromSource(source, this.dateMinPropertyName);
                if (!minDate.HasValue)
                    return ValidationResult.Success;

                if (date < minDate)
                {
                    return ValidationResult.Fail("End date must be greater than begin date.");
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
