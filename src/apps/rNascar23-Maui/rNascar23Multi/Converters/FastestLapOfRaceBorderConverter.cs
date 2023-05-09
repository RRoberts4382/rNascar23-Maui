using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class FastestLapOfRaceBorderConverter : SelectedBorderConverter, IValueConverter
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
            if (Application.Current.Resources.TryGetValue("FastestLapOfRaceBorderColor", out object notSelectedColor))
            {
                return (Color)notSelectedColor;
            }

            return Colors.Transparent;
        }
    }
}
