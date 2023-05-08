using System.Globalization;

namespace rNascar23Multi.Converters
{
    internal class ManufacturerLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            switch (value.ToString())
            {
                case "Chv":
                    return ImageSource.FromFile("chevrolet.png");
                case "Frd":
                    return ImageSource.FromFile("ford.png");
                case "Tyt":
                    return ImageSource.FromFile("toyota.png");
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
