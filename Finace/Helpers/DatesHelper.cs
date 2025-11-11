using Finace.Models;

namespace Finace.Helpers
{
    public static class DatesHelper
    {
        public static Period GetCurrentMonth()
        {
            var today = DateTime.Now;

            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddSeconds(-1);

            if(endDate >  today) endDate = today.Date.AddDays(1).AddSeconds(-1);

            return new Period
            {
                startDate = startDate,
                endDate = endDate
            };
        }

        public static Period? GetMonthPeriodByDateAndMonth(int? year, int? month)
        {
            if (year == null || month == null) return GetCurrentMonth();

            if(year == 0)
                return null;
            if(month == 0)
            {
                return new Period
                {
                    startDate = new DateTime((int)year, 1, 1),
                    endDate = new DateTime((int)year + 1, 1, 1).AddSeconds(-1) < DateTime.Now ?
                        new DateTime((int)year + 1, 1, 1).AddSeconds(-1) :
                        DateTime.Now
                };
            }

            var today = DateTime.Now;

            var startDate = new DateTime((int)year, (int)month, 1);
            var endDate = new DateTime((int)year, (int)month, 1).AddMonths(1).AddSeconds(-1);

            if (endDate > today) endDate = today.Date.AddDays(1).AddSeconds(-1);

            return new Period
            {
                startDate = startDate,
                endDate = endDate
            };
        }
    }
}
