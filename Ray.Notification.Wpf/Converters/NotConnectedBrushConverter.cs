using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ray.Notification.Wpf.Converters
{
    public class NotConnectedBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = bool.Parse(value?.ToString() ?? "false");

            SolidColorBrush result;

            if (boolValue)
            {
                result = (SolidColorBrush)(new BrushConverter().ConvertFrom("#28A1A2"));
            }
            else
            {
                result = Brushes.DarkRed;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
