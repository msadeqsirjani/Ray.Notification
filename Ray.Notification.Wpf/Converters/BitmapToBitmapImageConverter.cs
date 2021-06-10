using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ray.Notification.Wpf.Converters
{
    public class BitmapToBitmapImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                var imageBrush = (ImageBrush)Application.Current.Resources["SettingsLogoPrincipalBrush"];

                return imageBrush;
            }

            var logo = new BitmapImage();

            logo.BeginInit();

            var path = value.ToString();

            logo.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            logo.EndInit();

            var result = new ImageBrush(logo);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private BitmapImage ToImage(byte[] array)
        {
            using (var stream = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();

                image.BeginInit();

                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;

                image.EndInit();

                return image;
            }
        }
    }
}
