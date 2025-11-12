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
        #region Property
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _unitAverage = "день";
        public string UnitAverage
        {
            get => _unitAverage;
            set { _unitAverage = value; OnPropertyChanged(); }
        }

        private string _daysOrMonthsForAverage;
        public string DaysOrMonthsForAverage
        {
            get => _daysOrMonthsForAverage;
            set
            {
                if (_daysOrMonthsForAverage != value)
                {
                    _daysOrMonthsForAverage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DaysOrMonthsForAverage));
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
                    OnPropertyChanged(nameof(IncludeTagsCheckBox));
                }
            }
        }

        private ObservableCollection<CategoryAmount> _totalCostList;
        public ObservableCollection<CategoryAmount> TotalCostList
        {
            get { return _totalCostList; }
            set
            {
                _totalCostList = value;
                OnPropertyChanged(nameof(TotalCostList));
            }
        }

        private ObservableCollection<CategoryAmount> _averageCostList;
        public ObservableCollection<CategoryAmount> AverageCostList
        {
            get { return _averageCostList; }
            set
            {
                _averageCostList = value;
                OnPropertyChanged(nameof(AverageCostList));
            }
        }

        private ObservableCollection<CategoryAmount> _totalIncomeList;
        public ObservableCollection<CategoryAmount> TotalIncomeList
        {
            get { return _totalIncomeList; }
            set
            {
                _totalIncomeList = value;
                OnPropertyChanged(nameof(TotalIncomeList));
            }
        }

        private ObservableCollection<CategoryAmount> _necessarilyList;
        public ObservableCollection<CategoryAmount> AverageIncomeList
        {
            get { return _necessarilyList; }
            set
            {
                _necessarilyList = value;
                OnPropertyChanged(nameof(AverageIncomeList));
            }
        }

        public ISeries[] _series_Balance;
        public ISeries[] Series_Balance
        {
            get => _series_Balance;
            set
            {
                if (_series_Balance != value)
                {
                    _series_Balance = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Series_Balance));
                }
            }
        }

        private Func<double, string> _dateFormatter;
        public Func<double, string> DateFormatter
        {
            get => _dateFormatter;
            set
            {
                _dateFormatter = value;
                OnPropertyChanged();
            }
        }

        public Axis[] XAxes => new Axis[]
        {
            new Axis
            {
                Labeler = DateFormatter,
                UnitWidth = TimeSpan.FromDays(1).Ticks
            }
        };

        public Axis[] YAxes => new Axis[]
        {
            new Axis()
        };

        private bool _anyDateCheckBox;
        public bool AnyDateCheckBox
        {
            get => _anyDateCheckBox;
            set
            {
                if (_anyDateCheckBox != value)
                {
                    _anyDateCheckBox = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(AnyDateCheckBox));
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
                    OnPropertyChanged(nameof(DateFrom));
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
                    OnPropertyChanged(nameof(DateTo));
                }
            }
        }

        public ObservableCollection<int> Months { get; set; }
        public ObservableCollection<int> Years { get; set; }

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
            DateFormatter = value => new DateTime((long)value).ToString("dd MMM");
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

            Series_Balance = new ISeries[]
            {
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
            };
            UpdateCategories();
        }
        
        public void UpdateCategories()
        {
            var period = CalculatePeriod();

            var totalCostList = _statisticService.CategoryExpensesForPeriod(period, IncludeTagsCheckBox);
            var totalIncomeList = _statisticService.CategoryIncomeForPeriod(period, IncludeTagsCheckBox);

            totalCostList.Add(new CategoryAmount { Category = "__СУММА__", Amount = totalCostList.Sum(v => v.Amount) });
            totalIncomeList.Add(new CategoryAmount { Category = "__СУММА__", Amount = totalIncomeList.Sum(v => v.Amount) });

            TotalCostList = new ObservableCollection<CategoryAmount>(totalCostList.OrderByDescending(e => e.Amount).ToList());
            TotalIncomeList = new ObservableCollection<CategoryAmount>(totalIncomeList.OrderByDescending(e => e.Amount).ToList());

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

            AverageCostList = new ObservableCollection<CategoryAmount>(
                totalCostList
                    .Select(e => new CategoryAmount
                    {
                        Category = e.Category,
                        Amount = e.Amount * factor
                    })
                    .OrderByDescending(e => e.Amount)
                    .ToList()
            );

            AverageIncomeList = new ObservableCollection<CategoryAmount>(
                totalIncomeList
                    .Select(e => new CategoryAmount
                    {
                        Category = e.Category,
                        Amount = e.Amount * factor
                    })
                    .OrderByDescending(e => e.Amount)
                    .ToList()
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
