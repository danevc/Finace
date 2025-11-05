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
                    < 70 => new SolidColorBrush(Color.FromRgb(76, 175, 80)),    // Зеленый
                    < 90 => new SolidColorBrush(Color.FromRgb(255, 152, 0)),    // Оранжевый
                    _ => new SolidColorBrush(Color.FromRgb(244, 67, 54))        // Красный
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
