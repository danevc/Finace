using Finace.Models;
using Finace.Options;
using Finace.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace Finace.Service
{
    public class BudgetService : IBudgetService
    {
        private ITransactionsService _transactionService;
        private List<Transaction>? _allTransactions;
        private Settings _settings;

        public BudgetService(ITransactionsService transactionService, IOptions<Settings> configuration)
        {
            _settings = configuration.Value;
            _transactionService = transactionService;
            _allTransactions = transactionService.Transactions;
        }

        public List<ExpenseCategory> GetBudgetNecessarilyForPeriod(Period period)
        {
            if(_allTransactions is null || !_allTransactions.Any()) return new List<ExpenseCategory>();

            var transactionsNecessarily = _allTransactions
                .Where(r => r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount < 0)
                .Where(r => r.Note != "Корректировка остатка")
                .Where(r => _settings.CategoriesNecessarily?.Contains(r.Category ?? "") ?? false)
                .Where(r => !(_settings.Tags?.Contains(r.Tags ?? "") ?? false));

            if (!transactionsNecessarily.Any()) return new List<ExpenseCategory>();

            return transactionsNecessarily.GroupBy(r => r.Category).Select(e => new ExpenseCategory
            {
                Category = e.Key ?? string.Empty,
                Amount = Math.Abs(e.Sum(x => x.Amount))
            }).ToList();
        }

        public List<ExpenseCategory> GetBudgetNotNecessarilyForPeriod(Period period)
        {
            if (_allTransactions is null || !_allTransactions.Any()) return new List<ExpenseCategory>();

            var transactionsNecessarily = _allTransactions
                .Where(r => r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount < 0)
                .Where(r => r.Note != "Корректировка остатка")
                .Where(r => !(_settings.CategoriesNecessarily?.Contains(r.Category ?? "") ?? false))
                .Where(r => !(_settings.Tags?.Contains(r.Tags ?? "") ?? false));

            if (!transactionsNecessarily.Any()) return new List<ExpenseCategory>();

            return transactionsNecessarily.GroupBy(r => r.Category).Select(e => new ExpenseCategory
            {
                Category = e.Key ?? string.Empty,
                Amount = Math.Abs(e.Sum(x => x.Amount))
            }).ToList();
        }

        public List<ExpenseCategory> GetTotalCostForPeriod(Period period)
        {
            if (_allTransactions is null || !_allTransactions.Any()) return new List<ExpenseCategory>();

            var transactionsNecessarily = _allTransactions
                .Where(r => r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount < 0)
                .Where(r => r.Note != "Корректировка остатка");

            if (!transactionsNecessarily.Any()) return new List<ExpenseCategory>();

            return transactionsNecessarily.GroupBy(r => r.Category).Select(e => new ExpenseCategory
            {
                Category = e.Key ?? string.Empty,
                Amount = Math.Abs(e.Sum(x => x.Amount))
            }).ToList();
        }

        public List<ExpenseCategory> GetTotalIncomeForPeriod(Period period)
        {
            if (_allTransactions is null || !_allTransactions.Any()) return new List<ExpenseCategory>();

            var transactionsNecessarily = _allTransactions
                .Where(r => r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount > 0)
                .Where(r => r.Note != "Корректировка остатка");

            if (!transactionsNecessarily.Any()) return new List<ExpenseCategory>();

            return transactionsNecessarily.GroupBy(r => r.Category).Select(e => new ExpenseCategory
            {
                Category = e.Key ?? string.Empty,
                Amount = Math.Abs(e.Sum(x => x.Amount))
            }).ToList();
        }
    }
}
