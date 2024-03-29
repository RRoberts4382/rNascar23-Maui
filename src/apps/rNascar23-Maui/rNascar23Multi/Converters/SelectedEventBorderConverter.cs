﻿using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class SelectedEventBorderConverter : SelectedBorderConverter, IValueConverter
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
