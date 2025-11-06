using Finace.Models;

namespace Finace.Service.Interfaces
{
    public interface IBudgetService
    {
        public List<ExpenseCategory> GetBudgetNecessarilyForPeriod(Period period);

        public List<ExpenseCategory> GetBudgetNotNecessarilyForPeriod(Period period);

        public List<ExpenseCategory> GetTotalCostForPeriod(Period period);

        public List<ExpenseCategory> GetTotalIncomeForPeriod(Period period);
    }
}
