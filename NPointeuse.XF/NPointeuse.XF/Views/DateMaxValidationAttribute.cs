using NPointeuse.Infra.Client;
using System;

namespace NPointeuse.XF.Views
{
    internal class DateMaxValidationAttribute : DateRangeValidationAttribute
    {
        private readonly string dateMaxPropertyName;

        public DateMaxValidationAttribute(string dateMaxPropertyName)
        {
            this.dateMaxPropertyName = dateMaxPropertyName;
        }

        public override ValidationResult IsValid(object source, object value)
        {
            try
            {
                var date = this.GetDate(value as string);
                if (!date.HasValue)
                    return ValidationResult.Success;

                var minDate = this.GetDateFromSource(source, this.dateMaxPropertyName);
                if (!minDate.HasValue)
                    return ValidationResult.Success;

                if (date > minDate)
                {
                    return ValidationResult.Fail("Begin date must be lower than end date");
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
