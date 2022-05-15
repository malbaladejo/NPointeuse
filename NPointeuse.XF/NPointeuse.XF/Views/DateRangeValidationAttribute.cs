using NPointeuse.Infra.Client;
using System;

namespace NPointeuse.XF.Views
{
    internal abstract class DateRangeValidationAttribute : ValidationAttribute
    {
        protected DateTime? GetDateFromSource(object source, string propertyName)
        {
            var property = source.GetType().GetProperty(propertyName);
            if (property == null)
                return null;

            var minDate = property.GetValue(source) as string;
            return this.GetDate(minDate);
        }

        protected DateTime? GetDate(string value)
        {
            if (DateTime.TryParse(value, out var date))
                return date;

            return null;
        }
    }
}
