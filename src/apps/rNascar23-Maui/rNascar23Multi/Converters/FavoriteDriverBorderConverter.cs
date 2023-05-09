using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class FavoriteDriverBorderConverter : SelectedBorderConverter, IValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ?
                    GetSelectedColor() :
                    GetNotSelectedColor();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        protected override Color GetSelectedColor()
        {
            if (Application.Current.Resources.TryGetValue("FavoriteDriverBorderColor", out object notSelectedColor))
            {
                return (Color)notSelectedColor;
            }

            return Colors.Transparent;
        }

        protected override Color GetNotSelectedColor()
        {
            if (Application.Current.Resources.TryGetValue("NotSelectedItemBorderColor", out object notSelectedColor))
            {
                return (Color)notSelectedColor;
            }

            return Colors.Transparent;
        }
    }
}
