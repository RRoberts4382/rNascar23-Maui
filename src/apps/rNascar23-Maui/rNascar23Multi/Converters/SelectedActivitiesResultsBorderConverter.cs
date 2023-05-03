using rNascar23Multi.Models;
using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class SelectedActivitiesResultsBorderConverter : SelectedBorderConverter, IValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(parameter.ToString(), out int parameterInt))
            {
                var buttonViewType = (ActivitiesResultsTypes)parameterInt;

                var selectedViewType = (ActivitiesResultsTypes)value;

                return buttonViewType == selectedViewType ?
                    GetSelectedColor() :
                    GetNotSelectedColor();
            }

            return GetNotSelectedColor();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
