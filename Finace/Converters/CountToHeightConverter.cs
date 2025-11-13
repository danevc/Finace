using System.Globalization;
using System.Windows.Data;

namespace Finace.Converters
{
    public class CountToHeightConverter : IValueConverter
    {
        public double ItemHeight { get; set; } = 28; // высота одной подкатегории

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count)
                return count * ItemHeight;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
