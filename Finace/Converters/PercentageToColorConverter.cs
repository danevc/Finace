using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Finace.Converters
{
    public class PercentageToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double percentage)
            {
                // Меняем цвет в зависимости от процента заполнения
                return percentage switch
                {
                    < 90 => new SolidColorBrush(Color.FromRgb(69, 108, 60)),
                    < 110 => new SolidColorBrush(Color.FromRgb(105, 42, 15)),
                    _ => new SolidColorBrush(Color.FromRgb(105, 15, 17)) 
                };
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
