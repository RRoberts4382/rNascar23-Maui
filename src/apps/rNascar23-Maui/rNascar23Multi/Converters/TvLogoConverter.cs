using rNascar23.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rNascar23Multi.Converters
{
    public class TvLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TvTypes)value)
            {
                case TvTypes.NBC:
                    return ImageSource.FromFile("nbc.png");
                case TvTypes.CNBC:
                    return ImageSource.FromFile("cnbc.png");
                case TvTypes.NBCSN:
                    return ImageSource.FromFile("nbcsn.png");
                case TvTypes.Fox:
                    return ImageSource.FromFile("fox.png");
                case TvTypes.FS1:
                    return ImageSource.FromFile("fs1.png");
                case TvTypes.FS2:
                    return ImageSource.FromFile("fs2.png");
                case TvTypes.USA:
                    return ImageSource.FromFile("usa.png");
                case TvTypes.Unknown:
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
