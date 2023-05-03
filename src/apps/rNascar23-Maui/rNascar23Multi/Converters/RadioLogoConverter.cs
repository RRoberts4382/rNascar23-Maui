using rNascar23.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rNascar23Multi.Converters
{
    public class RadioLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((RadioTypes)value)
            {
                case RadioTypes.PRN:
                    return ImageSource.FromFile("prn_radio.png");
                case RadioTypes.MRN:
                    return ImageSource.FromFile("mrn.png");
                case RadioTypes.IMS:
                    return ImageSource.FromFile("ims.png");
                case RadioTypes.Unknown:
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
