using rNascar23.Sdk.Common;
using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class SeriesLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((SeriesTypes)value)
            {
                case SeriesTypes.Cup:
                    return ImageSource.FromFile("ncs_small.png");
                case SeriesTypes.Xfinity:
                    return ImageSource.FromFile("xfinity_small.png");
                case SeriesTypes.Truck:
                    return ImageSource.FromFile("cts_small.png");
                case SeriesTypes.Other:
                case SeriesTypes.Unknown:
                default:
                    return ImageSource.FromFile("rnascar_logo_small.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
