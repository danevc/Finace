using Finace.Helpers;
using Finace.Options;
using Finace.Service.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Finace.ViewModels
{
    public interface IBudgetViewModel 
    {
        public void UpdatePage();
    }

    public class BudgetViewModel : IBudgetViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Property

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

                    // Если нужно обновить зависимые свойства
                    OnPropertyChanged(nameof(IncludeTagsCheckBox));
                }
            }
        }

        private ObservableCollection<ParentCategoryViewModel> _notNecessarilyList;
        public ObservableCollection<ParentCategoryViewModel> NotNecessarilyList
        {
            get { return _notNecessarilyList; }
            set
            {
                _notNecessarilyList = value;
                OnPropertyChanged(nameof(NotNecessarilyList));
            }
        }

        private ObservableCollection<ParentCategoryViewModel> _necessarilyList;
        public ObservableCollection<ParentCategoryViewModel> NecessarilyList
        {
            get { return _necessarilyList; }
            set
            {
                _necessarilyList = value;
                OnPropertyChanged(nameof(NecessarilyList));
            }
        }

        private ObservableCollection<ParentCategoryViewModel> _totalCostList;
        public ObservableCollection<ParentCategoryViewModel> TotalCostList
        {
            get { return _totalCostList; }
            set
            {
                _totalCostList = value;
                OnPropertyChanged(nameof(TotalCostList));
            }
        }

        private double _notNecessarilyPercentage;
        public double NotNecessarilyPercentage
        {
            get => _notNecessarilyPercentage;
            set
            {
                if (_notNecessarilyPercentage != value)
                {
                    _notNecessarilyPercentage = value;
                    OnPropertyChanged();

                    // Если нужно обновить зависимые свойства
                    OnPropertyChanged(nameof(NotNecessarilyProgressBarText));
                }
            }
        }

        private double _necessarilyPercentage;
        public double NecessarilyPercentage
        {
            get => _necessarilyPercentage;
            set
            {
                if (_necessarilyPercentage != value)
                {
                    _necessarilyPercentage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NecessarilyProgressBarText));
                }
            }
        }

        private double _totalCostToTotalIncomePercentage;
        public double TotalCostToTotalIncomePercentage
        {
            get => _totalCostToTotalIncomePercentage;
            set
            {
                if (_totalCostToTotalIncomePercentage != value)
                {
                    _totalCostToTotalIncomePercentage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalCostToTotalIncomeProgressBarText));
                }
            }
        }

        private string _necessarilyProgressBarText;
        public string NecessarilyProgressBarText
        {
            get => _necessarilyProgressBarText;
            set
            {
                if (_necessarilyProgressBarText != value)
                {
                    _necessarilyProgressBarText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _notNecessarilyProgressBarText;
        public string NotNecessarilyProgressBarText
        {
            get => _notNecessarilyProgressBarText;
            set
            {
                if (_notNecessarilyProgressBarText != value)
                {
                    _notNecessarilyProgressBarText = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _totalCostToTotalIncomeProgressBarText;
        public string TotalCostToTotalIncomeProgressBarText
        {
            get => _totalCostToTotalIncomeProgressBarText;
            set
            {
                if (_totalCostToTotalIncomeProgressBarText != value)
                {
                    _totalCostToTotalIncomeProgressBarText = value;
                    OnPropertyChanged();
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

        private readonly Settings _config;
        private readonly IStatisticService _statisticService;
        #endregion

        public BudgetViewModel(IOptions<Settings> configuration, IStatisticService statisticService)
        {
            _config = configuration.Value;
            _statisticService = statisticService;

            initDates();
        }

        public void UpdatePage()
        {
            var period = DatesHelper.GetMonthPeriodByDateAndMonth(SelectedYear, SelectedMonth);

            var necessarilyList = _statisticService.BudgetNecessarilyForPeriod(period, IncludeTagsCheckBox);
            var notNecessarilyList = _statisticService.BudgetNotNecessarilyForPeriod(period, IncludeTagsCheckBox);
            var totalCostList = _statisticService.CategoryExpensesForPeriod(period, IncludeTagsCheckBox);
            var totalIncomeList = _statisticService.CategoryIncomeForPeriod(period, IncludeTagsCheckBox);

            var necessarilySum = (double)necessarilyList.Sum(e => e.Amount);
            var notNecessarilySum = (double)notNecessarilyList.Sum(e => e.Amount);

            var totalCostSum = (double)totalCostList.Sum(e => e.Amount);
            var totalIncomeSum = (double)totalIncomeList.Sum(e => e.Amount);

            var necessarilyPrecentage = (double)(necessarilySum / _config.TotalBudgetNecessarily ?? 0);
            var notNecessarilyPrecentage = (double)(notNecessarilySum / _config.TotalBudgetNotNecessarily  ?? 0);
            var totalPrecentage = (double)(totalCostSum / totalIncomeSum);

            NecessarilyPercentage = necessarilyPrecentage * 100;
            NotNecessarilyPercentage = notNecessarilyPrecentage * 100;
            TotalCostToTotalIncomePercentage = totalPrecentage * 100;

            NecessarilyProgressBarText = $"{necessarilySum:N0} / {_config.TotalBudgetNecessarily:N0} ({necessarilyPrecentage:P1})";
            NotNecessarilyProgressBarText = $"{notNecessarilySum:N0} / {_config.TotalBudgetNotNecessarily:N0} ({notNecessarilyPrecentage:P1})";
            TotalCostToTotalIncomeProgressBarText = $"{totalCostSum:N0} / {totalIncomeSum:N0} ({totalPrecentage:P1})";

            NecessarilyList = new ObservableCollection<ParentCategoryViewModel>(necessarilyList
                .GroupBy(x => x.ParentCategory)
                .Select(g => new ParentCategoryViewModel(
                    g.Key ?? "(No parent)",
                    g.Select(s => new SubCategoryViewModel(s.Category ?? "(No sub)", s.Amount))
                )).OrderByDescending(e => e.Total).ToList());

            NotNecessarilyList = new ObservableCollection<ParentCategoryViewModel>(notNecessarilyList
                .GroupBy(x => x.ParentCategory)
                .Select(g => new ParentCategoryViewModel(
                    g.Key ?? "(No parent)",
                    g.Select(s => new SubCategoryViewModel(s.Category ?? "(No sub)", s.Amount))
                )).OrderByDescending(e => e.Total).ToList());

            TotalCostList = new ObservableCollection<ParentCategoryViewModel>(totalCostList
                .GroupBy(x => x.ParentCategory)
                .Select(g => new ParentCategoryViewModel(
                    g.Key ?? "(No parent)",
                    g.Select(s => new SubCategoryViewModel(s.Category ?? "(No sub)", s.Amount))
                )).OrderByDescending(e => e.Total).ToList());

        }

        private void initDates()
        {
            Months = new ObservableCollection<int>();
            Years = new ObservableCollection<int>();

            for (int i = 2025; i <= DateTime.Now.Year; i++)
                Years.Add(i);

            for (int i = 1; i <= 12; i++)
                Months.Add(i);

            SelectedMonth = Months[DateTime.Now.Month - 1];
            SelectedYear = DateTime.Now.Year;
        }
    }
}
