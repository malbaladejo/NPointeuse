using NPointeuse.Infra.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace NPointeuse.Infra.XF
{
    public class ValidationIssuesToStringConverter : IValueConverter
    {
        private static readonly Lazy<ValidationIssuesToStringConverter> lazy = new Lazy<ValidationIssuesToStringConverter>(() => new ValidationIssuesToStringConverter());

        public static ValidationIssuesToStringConverter Instance => lazy.Value;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var errors = value as IReadOnlyCollection<ValidationIssue>;
            if (errors == null)
                return value;

            return this.Convert(errors, parameter as string);
        }

        public string Convert(IReadOnlyCollection<ValidationIssue> errors, string propertyName)
        {
            return ConvertToString(errors.Where(e => string.IsNullOrEmpty(propertyName) || e.PropertyName == propertyName));
        }

        private static string ConvertToString(IEnumerable<ValidationIssue> errors)
            => string.Join(Environment.NewLine, errors.Select(e => e.Message));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
