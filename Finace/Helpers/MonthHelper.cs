using Finace.Models;

namespace Finace.Helpers
{
    public static class MonthHelper
    {
        public static Period GetCurrentMonth()
        {
            var today = DateTime.Now;

            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddSeconds(-1);

            if(endDate >  today) endDate = today;

            return new Period
            {
                startDate = startDate,
                endDate = endDate
            };
        }

        public static Period GetMonthPeriodByDateAndMonth(int? year, int? month)
        {
            if (year == null || month == null) return GetCurrentMonth();

            var today = DateTime.Now;

            var startDate = new DateTime((int)year, (int)month, 1);
            var endDate = new DateTime((int)year, (int)month, 1).AddMonths(1).AddSeconds(-1);

            if (endDate > today) endDate = today;

            return new Period
            {
                startDate = startDate,
                endDate = endDate
            };
        }
    }
}
