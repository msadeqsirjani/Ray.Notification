using Ray.Notification.Domain;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Ray.Notification.Wpf.Converters
{
    public class NotificationTypeImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueData = value ?? 0;

            var dataType = (NotificationType)valueData;

            string path;

            switch (dataType)
            {
                case NotificationType.Information:
                    path = @"/#28A1A2;component/Images/information.png";
                    break;
                case NotificationType.Warning:
                    path = @"/#28A1A2;component/Images/warining.png";
                    break;
                case NotificationType.Error:
                    path = @"/#28A1A2;component/Images/error.png";
                    break;
                case NotificationType.VeryImportant:
                    path = @"/#28A1A2;component/Images/veryimportant.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var logo = new BitmapImage();

            logo.BeginInit();

            logo.UriSource = new Uri(path, UriKind.Relative);

            logo.EndInit();

            return logo;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
