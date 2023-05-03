using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class GainLossColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                return GetColor((int)value);
            else
                return GetColor((float)value);
        }

        protected virtual Color GetColor(int value)
        {
            if (value > 0)
                return Colors.Green;
            else if (value < 0)
                return Colors.Red;
            else
            {
                if (Application.Current.Resources.TryGetValue("PrimaryTextColor", out object selectedColor))
                    return (Color)selectedColor;
                else
                    return Colors.Gray;
            }
        }

        protected virtual Color GetColor(float value)
        {
            if (value > 0)
                return Colors.Green;
            else if (value < 0)
                return Colors.Red;
            else
            {
                if (Application.Current.Resources.TryGetValue("PrimaryTextColor", out object selectedColor))
                    return (Color)selectedColor;
                else
                    return Colors.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
