using Finace.Helpers;
using Finace.Models;
using Finace.Service.Interfaces;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        private string _daysForAverage;
        public string DaysForAverage
        {
            get => _daysForAverage;
            set
            {
                if (_daysForAverage != value)
                {
                    _daysForAverage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DaysForAverage));
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
            }
        }

        private readonly ILogger<MainViewModel> _logger;
        private readonly IConfiguration _config;
        private readonly IStatisticService _service;

        #endregion

        public DashboardViewModel(ILogger<MainViewModel> logger, IConfiguration configuration, IStatisticService service)
        {
            _logger = logger;
            _config = configuration;
            _service = service;
            initDates();
            DateFormatter = value => new DateTime((long)value).ToString("dd MMM");
            DaysForAverage = "30,44";
        }

        public void UpdatePage()
        {
            var period = AnyDateCheckBox ?
                new Models.Period { startDate = DateFrom, endDate = DateTo }
                :  DatesHelper.GetMonthPeriodByDateAndMonth(SelectedYear, SelectedMonth);

            var values = _service.BalanceForPeriod(period);

            Series_Balance = new ISeries[]
            {
                new LineSeries<DateTimePoint>
                {
                    Name = "Баланс",
                    Values = values.OrderBy(e => e.Date)
                                   .Select(v => new DateTimePoint(v.Date, v.Amount))
                                   .ToArray(),
                    Stroke = new SolidColorPaint(SKColors.LightBlue, 2),
                    GeometrySize = 10
                }
            };
            UpdateCategories();
        }
        
        public void UpdateCategories()
        {
            var period = AnyDateCheckBox ?
                new Models.Period { startDate = DateFrom, endDate = DateTo }
                : DatesHelper.GetMonthPeriodByDateAndMonth(SelectedYear, SelectedMonth);

            var totalCostList = _service.CategoryExpensesForPeriod(period, IncludeTagsCheckBox);
            var totalIncomeList = _service.CategoryIncomeForPeriod(period, IncludeTagsCheckBox);

            totalCostList.Add(new CategoryAmount { Category = "__СУММА__", Amount = totalCostList.Sum(v => v.Amount) });
            totalIncomeList.Add(new CategoryAmount { Category = "__СУММА__", Amount = totalIncomeList.Sum(v => v.Amount) });

            TotalCostList = new ObservableCollection<CategoryAmount>(totalCostList.OrderByDescending(e => e.Amount).ToList());
            TotalIncomeList = new ObservableCollection<CategoryAmount>(totalIncomeList.OrderByDescending(e => e.Amount).ToList());

            double daysBetween = period != null ? (period.endDate - period.startDate)!.Value.Days : 365; //тут разница между датой первой записи и текущей

            AverageCostList = new ObservableCollection<CategoryAmount>(totalCostList.Select(e => new CategoryAmount {
                Category = e.Category,
                Amount = e.Amount / daysBetween * Convert.ToDouble(DaysForAverage)
            }).OrderByDescending(e => e.Amount).ToList());

            AverageIncomeList = new ObservableCollection<CategoryAmount>(totalIncomeList.Select(e => new CategoryAmount
            {
                Category = e.Category,
                Amount = e.Amount / daysBetween * Convert.ToDouble(DaysForAverage)
            }).OrderByDescending(e => e.Amount).ToList());
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

            SelectedMonth = Months[period.startDate!.Value.Month - 1];
            SelectedYear = period.startDate!.Value.Year;
            DateFrom = period.startDate;
            DateTo = period.endDate;
        }
    }
}
