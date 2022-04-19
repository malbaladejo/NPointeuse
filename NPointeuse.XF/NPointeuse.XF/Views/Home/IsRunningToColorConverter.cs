using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace NPointeuse.XF.Views.Home
{
    internal class IsRunningToColorConverter : IValueConverter
    {
        private static readonly Lazy<IsRunningToColorConverter> instance = new Lazy<IsRunningToColorConverter>(() => new IsRunningToColorConverter());

        public static IsRunningToColorConverter Instance => instance.Value;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool isRunning)
            {
                return isRunning ? Color.Red : Color.Green;
            }

            return Color.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
