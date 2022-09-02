using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace CV19Statistics.Infrastructure.Converters
{
    internal class LocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;
            return $"(lat: {point.X}; lon: {point.Y})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string str)) return null;

            string[] s = str.Split(";");
            double lat = double.Parse(s[0].Substring(5));
            double lon = double.Parse(s[1].Substring(5));

            return new Point(lat, lon);
        }
    }
}
