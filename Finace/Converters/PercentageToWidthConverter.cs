using System.Globalization;
using System.Windows.Data;

namespace Finace.Converters
{
    public class PercentageToWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is double percentage && values[1] is double actualWidth)
            {
                // Вычисляем ширину прогресс-бара
                var width = actualWidth * (percentage / 100.0);
                return Math.Min(width, actualWidth); // Не больше 100%
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
