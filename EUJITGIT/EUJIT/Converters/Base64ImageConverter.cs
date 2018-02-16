using System;
using System.IO;
using Xamarin.Forms;

namespace EUJIT.Convertors
{
    public class Base64ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {


            ImageSource retSource = null;

            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }

            return retSource;


            //string s = value as string;

            //if (s == null)
            //    return null;


         

            //bi.BeginInit();
            //bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(s));
            //bi.EndInit();

            //return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
