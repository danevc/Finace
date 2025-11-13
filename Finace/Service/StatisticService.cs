using Finace.Models;
using Finace.Options;
using Finace.Service.Interfaces;
using Microsoft.Extensions.Options;

namespace Finace.Service
{
    public class StatisticService : IStatisticService
    {
        private List<Transaction>? _allTransactions;
        private Settings _settings;

        public StatisticService(ITransactionsService transactionService, IOptions<Settings> configuration)
        {
            _settings = configuration.Value;
            _allTransactions = transactionService.Transactions;
        }

        public List<CategoryAmount> BudgetNecessarilyForPeriod(Period? period, bool includeTags)
        {
            if (_allTransactions is null || !_allTransactions.Any()) return new List<CategoryAmount>();

            var transactionsNecessarily = _allTransactions
                .Where(r => period == null || r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount < 0)
                .Where(r => r.Note != "Корректировка остатка")
                .Where(r => _settings.CategoriesNecessarily?.Contains(r.Category ?? "") ?? false);

            if (!transactionsNecessarily.Any()) return new List<CategoryAmount>();

            if (!includeTags) transactionsNecessarily = transactionsNecessarily.Where(r => !(_settings.Tags?.Contains(r.Tags ?? "") ?? false));

            return transactionsNecessarily.GroupBy(r => r.Category).Select(e => new CategoryAmount
            {
                ParentCategory = e.First().ParentCategory,
                Category = e.Key ?? string.Empty,
                Amount = (double)Math.Abs(e.Sum(x => x.Amount))
            }).ToList();
        }

        public List<CategoryAmount> BudgetNotNecessarilyForPeriod(Period? period, bool includeTags)
        {
            if (_allTransactions is null || !_allTransactions.Any()) return new List<CategoryAmount>();

            var transactionsNecessarily = _allTransactions
                .Where(r => period == null || r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount < 0)
                .Where(r => r.Note != "Корректировка остатка")
                .Where(r => !(_settings.CategoriesNecessarily?.Contains(r.Category ?? "") ?? false));

            if (!transactionsNecessarily.Any()) return new List<CategoryAmount>();

            if (!includeTags) transactionsNecessarily = transactionsNecessarily.Where(r => !(_settings.Tags?.Contains(r.Tags ?? "") ?? false));

            return transactionsNecessarily.GroupBy(r => r.Category).Select(e => new CategoryAmount
            {
                ParentCategory = e.First().ParentCategory,
                Category = e.Key ?? string.Empty,
                Amount = (double)Math.Abs(e.Sum(x => x.Amount))
            }).ToList();
        }

        public List<CategoryAmount> CategoryExpensesForPeriod(Period? period, bool includeTags, int take = int.MaxValue)
        {
            if (_allTransactions is null || !_allTransactions.Any()) return new List<CategoryAmount>();

            var transactionsNecessarily = _allTransactions
                .Where(r => period == null || r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount < 0)
                .Where(r => r.Note != "Корректировка остатка");

            if (!transactionsNecessarily.Any()) return new List<CategoryAmount>();

            if (!includeTags) transactionsNecessarily = transactionsNecessarily.Where(r => !(_settings.Tags?.Contains(r.Tags ?? "") ?? false));

            var result = transactionsNecessarily.GroupBy(r => r.Category).Select(e => new CategoryAmount
            {
                ParentCategory = e.First().ParentCategory,
                Category = e.Key ?? string.Empty,
                Amount = (double)Math.Abs(e.Sum(x => x.Amount))
            }).OrderByDescending(e => e.Amount).Take(take).ToList();

            return result;
        }

        public List<CategoryAmount> CategoryIncomeForPeriod(Period? period, bool includeTags)
        {
            if (_allTransactions is null || !_allTransactions.Any()) return new List<CategoryAmount>();

            var transactionsNecessarily = _allTransactions
                .Where(r => period == null || r.Date >= period.startDate && r.Date <= period.endDate)
                .Where(r => r.TransferAmount == null)
                .Where(r => r.Amount > 0)
                .Where(r => r.Note != "Корректировка остатка");

            if (!transactionsNecessarily.Any()) return new List<CategoryAmount>();

            if (!includeTags) transactionsNecessarily = transactionsNecessarily.Where(r => !(_settings.Tags?.Contains(r.Tags ?? "") ?? false));

            return transactionsNecessarily.GroupBy(r => r.Category).Select(e => new CategoryAmount
            {
                ParentCategory = e.First().ParentCategory,
                Category = e.Key ?? string.Empty,
                Amount = (double)Math.Abs(e.Sum(x => x.Amount))
            }).ToList();
        }

        public List<DataPoint> BalanceForPeriod(Period period)
        {
            var result = new List<DataPoint>();

            if (_allTransactions is null || !_allTransactions.Any()) return result;

            var transactionsForPeriod = _allTransactions
                .Where(r => r.Date >= period.startDate && r.Date <= period.endDate)
                .OrderBy(r => r.Date).ToList();

            var balance = _allTransactions
                .Where(r => r.Date < period.startDate)
                .Where(r => r.TransferAmount == null)
                .OrderBy(r => r.Date)
                .Sum(r => r.Amount);

            var totalDays = (period.endDate!.Value - period.startDate!.Value).TotalDays;
            var step = Convert.ToInt32(totalDays / 35);

            if (step < 1) step = 1;

            DateTime lastAddedDate = period.startDate.Value;

            result.Add(new DataPoint { Date = lastAddedDate, Amount = (double)balance });

            for (int i = 0; i < transactionsForPeriod.Count; i++)
            {
                var r = transactionsForPeriod[i];

                if (r.TransferAmount == null)
                    balance += r.Amount;

                var currentDate = r.Date;

                if ((currentDate - lastAddedDate).TotalDays >= step)
                {
                    result.Add(new DataPoint { Date = currentDate, Amount = (double)balance });
                    lastAddedDate = currentDate;
                }
            }

            if (lastAddedDate != period.endDate.Value)
                result.Add(new DataPoint { Date = period.endDate!.Value, Amount = (double)balance });

            return result;
        }
    }
}
