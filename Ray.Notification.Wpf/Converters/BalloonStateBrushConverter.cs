using Ray.Notification.Domain;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ray.Notification.Wpf.Converters
{
    public class BalloonStateBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var notificationType = (NotificationType)value;

            SolidColorBrush result;

            switch (notificationType)
            {
                case NotificationType.Information:
                case NotificationType.Warning:
                case NotificationType.Error:
                case NotificationType.VeryImportant:
                    result = Brushes.White;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
