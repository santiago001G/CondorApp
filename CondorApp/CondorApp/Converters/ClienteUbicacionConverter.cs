using CondorApp.Models;
using System;
using Xamarin.Forms;

namespace CondorApp.Converters
{
    public class ClienteUbicacionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null && values.Length == 2)
            {
                string latitud = values[0].ToString();
                string longitud = values[1].ToString();

                return new Cliente { Latitud = latitud, Logitud = longitud };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
