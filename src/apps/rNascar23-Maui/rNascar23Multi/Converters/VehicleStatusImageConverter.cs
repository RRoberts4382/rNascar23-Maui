using rNascar23.Sdk.Common;
using System.Globalization;

namespace rNascar23Multi.Converters
{
    internal class VehicleStatusImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            VehicleStatusTypes vehicleStatusType = (VehicleStatusTypes)value;

            switch (vehicleStatusType)
            {
                case VehicleStatusTypes.OnTrack:
                    return null;
                case VehicleStatusTypes.InPits:
                    return GetInPitsImage();
                case VehicleStatusTypes.Garage:
                    return GetInGarageImage();
                case VehicleStatusTypes.Retired:
                    return ImageSource.FromFile("retired.png");
                default:
                    return ImageSource.FromFile("rnascar_logo_small.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual ImageSource GetInPitsImage()
        {
            if (Application.Current.Resources.TryGetValue("InPitsStatus", out object statusImage))
            {
                return ImageSource.FromFile(statusImage.ToString());
            }

            return ImageSource.FromFile("rnascar_logo_small.png");
        }

        protected virtual ImageSource GetInGarageImage()
        {
            if (Application.Current.Resources.TryGetValue("InGarageStatus", out object statusImage))
            {
                return ImageSource.FromFile(statusImage.ToString());
            }

            return ImageSource.FromFile("rnascar_logo_small.png");
        }
    }
}
