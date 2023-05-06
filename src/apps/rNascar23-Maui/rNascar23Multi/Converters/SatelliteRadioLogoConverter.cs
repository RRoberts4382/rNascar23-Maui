using rNascar23.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rNascar23Multi.Converters
{
    public class SatelliteRadioLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((SatelliteTypes)value)
            {
                case SatelliteTypes.Sirius:
                    return ImageSource.FromFile("sirius.png");
                case SatelliteTypes.Unknown:
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
