using System.Globalization;

namespace rNascar23Multi.Converters
{
    class PitStopTiresConverter : IValueConverter
    {
        #region consts

        private const string RightSideTireChange = "TWO_WHEEL_CHANGE_RIGHT";
        private const string LeftSideTireChange = "TWO_WHEEL_CHANGE_LEFT";
        private const string FourTireChange = "FOUR_WHEEL_CHANGE";
        private const string NoTireChange = "OTHER";

        #endregion

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case RightSideTireChange:
                    return ImageSource.FromFile("right_tires.png");
                case LeftSideTireChange:
                    return ImageSource.FromFile("left_tires.png");
                case FourTireChange:
                    return ImageSource.FromFile("four_tires.png");
                case NoTireChange:
                default:
                    return null;//return ImageSource.FromFile("transparent_pixel.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
