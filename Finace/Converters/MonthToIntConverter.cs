using System.Globalization;
using System.Windows.Data;

namespace Finace.Converters
{
    public class MonthToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var result = value switch
            {
                1 => "Январь",
                2 => "Февраль",
                3 => "Март",
                4 => "Апрель",
                5 => "Май",
                6 => "Июнь",
                7 => "Июль",
                8 => "Август",
                9 => "Сентябрь",
                10 => "Октябрь",
                11 => "Ноябрь",
                12 => "Декабрь",
                _ => "-"
            };
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string monthName)
            {
                return monthName switch
                {
                    "Январь" => 1,
                    "Февраль" => 2,
                    "Март" => 3,
                    "Апрель" => 4,
                    "Май" => 5,
                    "Июнь" => 6,
                    "Июль" => 7,
                    "Август" => 8,
                    "Сентябрь" => 9,
                    "Октябрь" => 10,
                    "Ноябрь" => 11,
                    "Декабрь" => 12,
                    _ => 0
                };
            }
            return 0;
        }
    }
}
