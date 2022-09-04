using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using MapControl;

namespace CV19Statistics.Infrastructure.Converters
{
    internal class MapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Point point) return null;
            return new Location(point.X, point.Y);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Location location) return null;
            return new Point(location.Latitude, location.Longitude);
        }
    }
}