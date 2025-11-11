using Finace.Models;

namespace Finace.Service.Interfaces
{
    public interface IStatisticService
    {
        public List<DataPoint> BalanceForPeriod(Period? period);

        public List<CategoryAmount> BudgetNecessarilyForPeriod(Period? period, bool includeTags);

        public List<CategoryAmount> BudgetNotNecessarilyForPeriod(Period? period, bool includeTags);

        public List<CategoryAmount> CategoryExpensesForPeriod(Period? period, bool includeTags, int take = int.MaxValue);

        public List<CategoryAmount> CategoryIncomeForPeriod(Period? period, bool includeTags);
    }
}
