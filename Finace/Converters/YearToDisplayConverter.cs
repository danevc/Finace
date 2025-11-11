using System.Globalization;
using System.Windows.Data;

namespace Finace.Converters
{
    public class YearToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int year)
            {
                if (year == 0)
                    return "–";
                return year.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && s == "–")
                return 0;
            if (int.TryParse(value as string, out int year))
                return year;
            return 0;
        }
    }
}
