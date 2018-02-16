using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace EUJIT.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] bytes = value as byte[];
            ImageSource imgSource = ImageSource.FromStream(() => new MemoryStream(bytes));


            return imgSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
