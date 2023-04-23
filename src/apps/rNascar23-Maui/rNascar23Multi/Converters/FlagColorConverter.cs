using System.Globalization;

namespace rNascar23Multi.Converters
{
    public class FlagColorConverter : IValueConverter
    {
        public FlagColorConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return Colors.Black;
            }
            else if ((int)value == 1)
            {
                return Colors.Green;
            }
            else if ((int)value == 2)
            {
                return Colors.Gold;
            }
            else if ((int)value == 3)
            {
                return Colors.Red;
            }
            else if ((int)value == 4)
            {
                return Colors.White;
            }
            else if ((int)value == 8)
            {
                return Colors.DarkOrange;
            }
            else if ((int)value == 9)
            {
                return Colors.CornflowerBlue;
            }
            else
            {
                return Colors.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
