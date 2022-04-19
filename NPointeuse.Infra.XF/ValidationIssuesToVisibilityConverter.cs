using NPointeuse.Infra.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace NPointeuse.Infra.XF
{
    public class ValidationIssuesToBooleanConverter : IValueConverter
    {
        private static readonly Lazy<ValidationIssuesToBooleanConverter> lazy
            = new Lazy<ValidationIssuesToBooleanConverter>(() => new ValidationIssuesToBooleanConverter());

        public static ValidationIssuesToBooleanConverter Instance => lazy.Value;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var errors = value as IReadOnlyCollection<ValidationIssue>;
            if (errors == null)
                return value;

            return this.Convert(errors, parameter as string);
        }

        public bool Convert(IReadOnlyCollection<ValidationIssue> errors, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return errors.Any();

            return errors.Any(e => e.PropertyName == propertyName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
