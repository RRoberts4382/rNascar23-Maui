using rNascar23.Sdk.Common;
using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class SelectedScheduleTypeBorderConverter : SelectedBorderConverter, IValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(parameter.ToString(), out int parameterInt))
            {
                var buttonScheduleType = (ScheduleType)parameterInt;

                var selectedScheduleType = (ScheduleType)value;

                return buttonScheduleType == selectedScheduleType ?
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
