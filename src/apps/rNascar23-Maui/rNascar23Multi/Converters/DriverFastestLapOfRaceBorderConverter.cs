using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class DriverFastestLapOfRaceBorderConverter : SelectedBorderConverter, IValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ?
                    GetSelectedColor() :
                    Colors.Transparent;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected override Color GetSelectedColor()
        {
            if (Application.Current.Resources.TryGetValue("DriverFastestLapOfRaceBorderColor", out object notSelectedColor))
            {
                return (Color)notSelectedColor;
            }

            return Colors.Transparent;
        }
    }
}
