using Finace.Models;

namespace Finace.Helpers
{
    public static class MonthHelper
    {
        public static Period GetCurrentMonth()
        {
            var today = DateTime.Now;
            return new Period
            {
                startDate = new DateTime(today.Year, today.Month, 1),
                endDate = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddSeconds(-1)
            };
        }

        public static Period GetMonthPeriodByDateAndMonth(int? year, int? month)
        {
            if (year == null || month == null) return GetCurrentMonth();

            return new Period
            {
                startDate = new DateTime((int)year, (int)month, 1),
                endDate = new DateTime((int)year, (int)month, 1).AddMonths(1).AddSeconds(-1)
            };
        }
    }
}
