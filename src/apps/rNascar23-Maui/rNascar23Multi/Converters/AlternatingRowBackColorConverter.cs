using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class AlternatingRowBackColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is not int intValue
                ? Colors.Transparent
                : (intValue % 2) == 0
                    ? GetEvenColor()
                    : GetOddColor();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("NumberToAlternatingColorValueConverter.ConvertBack");
        }

        protected virtual Color GetEvenColor()
        {
            if (Application.Current.Resources.TryGetValue("AlternatingRowBackgroundColor", out object selectedColor))
            {
                return (Color)selectedColor;
            }

            return Colors.Transparent;
        }
        
        protected virtual Color GetOddColor()
        {
            if (Application.Current.Resources.TryGetValue("PrimaryRowBackgroundColor", out object notSelectedColor))
            {
                return (Color)notSelectedColor;
            }

            return Colors.Transparent;
        }
    }
}
