using System.Globalization;

namespace rNascar23Multi.Converters
{
    internal class NullableIntToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            else
                return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrEmpty(value.ToString()))
                return null;
            else
            {
                if (value != null && int.TryParse(value.ToString(), out int intValue))
                {
                    return intValue;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
