using System;
using System.Globalization;
using System.Windows.Data;

namespace Ray.Notification.Wpf.Converters
{
    public class NotConnectedTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = bool.Parse(value?.ToString() ?? "false");

            var result = boolValue ? "اعلان ها" : "اتصال بر قرار نیست";

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}