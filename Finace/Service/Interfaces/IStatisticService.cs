using Finace.Models;

namespace Finace.Service.Interfaces
{
    public interface IStatisticService
    {
        public List<DataPoint> GetTotalForPeriod(Period period);

        public List<ExpenseCategory> GetBudgetNecessarilyForPeriod(Period period, bool includeTags);

        public List<ExpenseCategory> GetBudgetNotNecessarilyForPeriod(Period period, bool includeTags);

        public List<ExpenseCategory> GetTotalCostForPeriod(Period period, bool includeTags);

        public List<ExpenseCategory> GetTotalIncomeForPeriod(Period period, bool includeTags);
    }
}
