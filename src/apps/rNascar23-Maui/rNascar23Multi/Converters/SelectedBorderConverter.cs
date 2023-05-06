using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rNascar23Multi.Converters
{
    public abstract class SelectedBorderConverter : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        protected virtual Color GetSelectedColor()
        {
            if (Application.Current.Resources.TryGetValue("SelectedItemBorderColor", out object selectedColor))
            {
                return (Color)selectedColor;
            }

            return GetNotSelectedColor();
        }

        protected virtual Color GetNotSelectedColor()
        {
            if (Application.Current.Resources.TryGetValue("NotSelectedItemBorderColor", out object notSelectedColor))
            {
                return (Color)notSelectedColor;
            }

            return Colors.Transparent;
        }
    }
}
