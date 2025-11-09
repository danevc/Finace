using Finace.Helpers;
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
    }

    public class DashboardViewModel : IDashboardViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public ISeries[] _series_TopCatgories;
        public ISeries[] Series_TopCatgories
        {
            get => _series_TopCatgories;
            set
            {
                if (_series_TopCatgories != value)
                {
                    _series_TopCatgories = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Series_TopCatgories));
                }
            }
        }

        public ISeries[] _series_DynamicCatgories;
        public ISeries[] Series_DynamicCatgories
        {
            get => _series_DynamicCatgories;
            set
            {
                if (_series_DynamicCatgories != value)
                {
                    _series_DynamicCatgories = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Series_DynamicCatgories));
                }
            }
        }

        public Axis[] XAxes { get; set; }
        public Axis[] YAxes { get; set; }

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


        public DashboardViewModel(ILogger<MainViewModel> logger, IConfiguration configuration, IStatisticService service)
        {
            _logger = logger;
            _config = configuration;
            _service = service;

            XAxes = new Axis[]
            {
                    new Axis
                    {
                        Labeler = value => new DateTime((long)value).ToString("dd.MM"),
                        UnitWidth = TimeSpan.FromDays(1).Ticks
                    }
            };
            YAxes = new Axis[]
            {
                new Axis()
            };

            initDates();
        }

        public void UpdatePage()
        {
            Models.Period period = AnyDateCheckBox ?
                new Models.Period { startDate = DateFrom, endDate = DateTo }
                : period = MonthHelper.GetMonthPeriodByDateAndMonth(SelectedYear, SelectedMonth);

            var values = _service.GetTotalForPeriod(period);

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

        }

        private void initDates()
        {
            Months = new ObservableCollection<int>();
            Years = new ObservableCollection<int>();

            for (int i = 2025; i <= DateTime.Now.Year; i++)
                Years.Add(i);

            for (int i = 1; i <= 12; i++)
                Months.Add(i);

            var period = MonthHelper.GetCurrentMonth();

            SelectedMonth = Months[period.startDate!.Value.Month - 1];
            SelectedYear = period.startDate!.Value.Year;
            DateFrom = period.startDate;
            DateTo = period.endDate;
        }
    }
}
