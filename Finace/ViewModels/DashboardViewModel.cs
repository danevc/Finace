using Finace.Helpers;
using Finace.Models;
using Finace.Service.Interfaces;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Finace.ViewModels
{
    public interface IDashboardViewModel
    {
        void UpdatePage();
        void UpdateCategories();
    }

    public class DashboardViewModel : IDashboardViewModel, INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? _unitAverage;
        public string? UnitAverage
        {
            get => _unitAverage;
            set 
            {
                if (_unitAverage != value)
                {
                    _unitAverage = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _totalCost;
        public double? TotalCost
        {
            get => _totalCost;
            set
            {
                if (_totalCost != value)
                {
                    _totalCost = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _totalIncome;
        public double? TotalIncome
        {
            get => _totalIncome;
            set
            {
                if (_totalIncome != value)
                {
                    _totalIncome = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _averageCost;
        public double? AverageCost
        {
            get => _averageCost;
            set
            {
                if (_averageCost != value)
                {
                    _averageCost = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _averageIncome;
        public double? AverageIncome
        {
            get => _averageIncome;
            set
            {
                if (_averageIncome != value)
                {
                    _averageIncome = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _daysOrMonthsForAverage;
        public string? DaysOrMonthsForAverage
        {
            get => _daysOrMonthsForAverage;
            set
            {
                if (_daysOrMonthsForAverage != value)
                {
                    _daysOrMonthsForAverage = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _includeTagsCheckBox;
        public bool IncludeTagsCheckBox
        {
            get => _includeTagsCheckBox;
            set
            {
                if (_includeTagsCheckBox != value)
                {
                    _includeTagsCheckBox = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<ParentCategoryViewModel>? _totalCostList;
        public ObservableCollection<ParentCategoryViewModel>? TotalCostList
        {
            get { return _totalCostList; }
            set
            {
                if (_totalCostList != value)
                {
                    _totalCostList = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<ParentCategoryViewModel>? _averageCostList;
        public ObservableCollection<ParentCategoryViewModel>? AverageCostList
        {
            get { return _averageCostList; }
            set
            {
                if (_averageCostList != value)
                {
                    _averageCostList = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<ParentCategoryViewModel>? _totalIncomeList;
        public ObservableCollection<ParentCategoryViewModel>? TotalIncomeList
        {
            get { return _totalIncomeList; }
            set
            {
                if (_totalIncomeList != value)
                {
                    _totalIncomeList = value;
                    OnPropertyChanged(nameof(TotalIncomeList));
                }
            }
        }

        private ObservableCollection<ParentCategoryViewModel>? _necessarilyList;
        public ObservableCollection<ParentCategoryViewModel>? AverageIncomeList
        {
            get { return _necessarilyList; }
            set
            {
                if (_necessarilyList != value)
                {
                    _necessarilyList = value;
                    OnPropertyChanged(nameof(AverageIncomeList));
                }
            }
        }

        public ISeries[]? _series_Balance;
        public ISeries[]? Series_Balance
        {
            get => _series_Balance;
            set
            {
                if (_series_Balance != value)
                {
                    if (_series_Balance != value)
                    {
                        _series_Balance = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        private Func<double, string> _dateFormatter = v => new DateTime((long) v).ToString("dd MMM");
        public Func<double, string> DateFormatter
        {
            get => _dateFormatter;
            set
            {
                _dateFormatter = value;
                OnPropertyChanged();
            }
        }

        public Axis[] XAxes =>
        [
            new() {
                Labeler = DateFormatter,
                UnitWidth = TimeSpan.FromDays(1).Ticks
            }
        ];

        public static Axis[] YAxes =>
        [
            new()
        ];

        private bool _anyDateCheckBox;
        public bool AnyDateCheckBox
        {
            get => _anyDateCheckBox;
            set
            {
                if (_anyDateCheckBox != value)
                {
                    if (_anyDateCheckBox != value)
                    {
                        _anyDateCheckBox = value;
                        OnPropertyChanged();
                    }
                }
            }
        }

        private DateTime? _dateFrom;
        public DateTime? DateFrom
        {
            get => _dateFrom;
            set
            {
                if (_dateFrom != value)
                {
                    _dateFrom = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _dateTo;
        public DateTime? DateTo
        {
            get => _dateTo;
            set
            {
                if (_dateTo != value)
                {
                    _dateTo = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<int>? Months { get; set; }
        public ObservableCollection<int>? Years { get; set; }

        private int? _selectedYear;
        public int? SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedYear)));

                var period = DatesHelper.GetMonthPeriodByDateAndMonth(_selectedYear, _selectedMonth);
                if (period == null) return;

                DateFrom = period.startDate;
                DateTo = period.endDate;
            }
        }

        private int? _selectedMonth;
        public int? SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMonth)));

                var period = DatesHelper.GetMonthPeriodByDateAndMonth(_selectedYear, _selectedMonth);
                if (period == null) return;

                DateFrom = period.startDate;
                DateTo = period.endDate;
            }
        }

        private readonly IStatisticService _statisticService;
        private readonly ITransactionsService _transactionService;

        #endregion

        public DashboardViewModel(IStatisticService statisticService, ITransactionsService transactionService)
        {
            _statisticService = statisticService;
            _transactionService = transactionService;

            initDates();
            DaysOrMonthsForAverage = "1";
            IncludeTagsCheckBox = true;
            UnitAverage = "месяц";
        }

        public void UpdatePage()
        {
            var period = CalculatePeriod();

            var values = _statisticService.BalanceForPeriod(period).OrderBy(e => e.Date)
                                   .Select(v => new DateTimePoint(v.Date, v.Amount))
                                   .ToArray();

            if (Series_Balance != null &&
                Series_Balance.Length > 0 &&
                Series_Balance[0].Values is IEnumerable<DateTimePoint> oldValues &&
                oldValues.Count() == values.Length &&
                oldValues.Zip(values, (a, b) => a.DateTime == b.DateTime && a.Value == b.Value).All(x => x))
            {
                return;
            }

            Series_Balance =
            [
                new LineSeries<DateTimePoint>
                {
                    Name = "Баланс",
                    Values = values,
                    Stroke = new SolidColorPaint(new SKColor(199, 157, 255), 2),
                    Fill = new SolidColorPaint(new SKColor(157, 119, 207, 55), 2),
                    GeometrySize = 10,
                    GeometryStroke = new SolidColorPaint(new SKColor(157, 119, 207), 2),
                    GeometryFill = new SolidColorPaint(new SKColor(255, 255, 255))
                }
            ];
            UpdateCategories();
        }
        
        public void UpdateCategories()
        {
            var period = CalculatePeriod();

            var totalCostList = _statisticService.CategoryExpensesForPeriod(period, IncludeTagsCheckBox);
            var totalIncomeList = _statisticService.CategoryIncomeForPeriod(period, IncludeTagsCheckBox);

            TotalCost = totalCostList.Sum(v => v.Amount);
            TotalIncome = totalIncomeList.Sum(v => v.Amount);

            TotalCostList = new ObservableCollection<ParentCategoryViewModel>(totalCostList
                .GroupBy(x => x.ParentCategory)
                .Select(g => new ParentCategoryViewModel(
                    g.Key ?? "(No parent)",
                    g.Select(s => new SubCategoryViewModel(s.Category ?? "(No sub)", s.Amount))
                )).OrderByDescending(e => e.Total).ToList());

            TotalIncomeList = new ObservableCollection<ParentCategoryViewModel>(totalIncomeList
                .GroupBy(x => x.ParentCategory)
                .Select(g => new ParentCategoryViewModel(
                    g.Key ?? "(No parent)",
                    g.Select(s => new SubCategoryViewModel(s.Category ?? "(No sub)", s.Amount))
                )).OrderByDescending(e => e.Total).ToList());

            var daysInPeriod = (int)Math.Round((period.endDate - period.startDate)!.Value.TotalDays);

            if (daysInPeriod == 0)
                daysInPeriod = 1;

            double factor;

            if (UnitAverage == "месяц")
            {
                double monthsBetween;

                if (daysInPeriod == DateTime.DaysInMonth(period.startDate!.Value.Year, period.startDate.Value.Month))
                    monthsBetween = 1;
                else
                    monthsBetween = daysInPeriod / 30.4375;
                factor = Convert.ToDouble(DaysOrMonthsForAverage) / monthsBetween;
            }
            else
            {
                factor = Convert.ToDouble(DaysOrMonthsForAverage) / daysInPeriod;
            }

            AverageCost = TotalCost * factor;
            AverageIncome = TotalIncome * factor;

            AverageCostList = new ObservableCollection<ParentCategoryViewModel>(
                totalCostList
                    .Select(e => new CategoryAmount
                    {
                        ParentCategory = e.ParentCategory,
                        Category = e.Category,
                        Amount = e.Amount * factor
                    })
                    .GroupBy(x => x.ParentCategory)
                    .Select(g => new ParentCategoryViewModel(
                    g.Key ?? "(No parent)",
                    g.Select(s => new SubCategoryViewModel(s.Category ?? "(No sub)", s.Amount))
                )).OrderByDescending(e => e.Total).ToList()
            );

            AverageIncomeList = new ObservableCollection<ParentCategoryViewModel>(
                totalIncomeList
                    .Select(e => new CategoryAmount
                    {
                        ParentCategory = e.ParentCategory,
                        Category = e.Category,
                        Amount = e.Amount * factor
                    })
                    .GroupBy(x => x.ParentCategory)
                    .Select(g => new ParentCategoryViewModel(
                    g.Key ?? "(No parent)",
                    g.Select(s => new SubCategoryViewModel(s.Category ?? "(No sub)", s.Amount))
                )).OrderByDescending(e => e.Total).ToList()
            );
        }

        private Period CalculatePeriod()
        {
            var period = AnyDateCheckBox ?
                new Models.Period { startDate = DateFrom, endDate = DateTo }
                : DatesHelper.GetMonthPeriodByDateAndMonth(SelectedYear, SelectedMonth);

            if (period == null)
                period = new Period { startDate = _transactionService.FirstTransactionDate!.Value, endDate = DateTime.Now };

            return period;
        }

        private void initDates()
        {
            Months = new ObservableCollection<int>();
            Years = new ObservableCollection<int>();

            Years.Add(0);

            for (int i = 2025; i <= DateTime.Now.Year; i++)
                Years.Add(i);

            Months.Add(0);

            for (int i = 1; i <= 12; i++)
                Months.Add(i);

            var period = DatesHelper.GetCurrentMonth();

            SelectedMonth = Months[period.startDate!.Value.Month];
            SelectedYear = period.startDate!.Value.Year;
            DateFrom = period.startDate;
            DateTo = period.endDate;
        }
    }
}
