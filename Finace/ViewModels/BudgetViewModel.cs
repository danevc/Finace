using Finace.Models;
using Finace.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace Finace.ViewModels
{
    public interface IBudgetViewModel
    {

    }

    public class BudgetViewModel : IBudgetViewModel
    {
        private List<ExpenseCategory> _notNecessarilyList;
        public List<ExpenseCategory> NotNecessarilyList { get { return _notNecessarilyList; } set { _notNecessarilyList = value; } }

        private List<ExpenseCategory> _necessarilyList;
        public List<ExpenseCategory> NecessarilyList { get { return _necessarilyList; } set { _necessarilyList = value; } }

        public double Percentage => 50;

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

        public ObservableCollection<string> Months { get; set; }
        private string? _selectedMonth;

        public string? SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMonth)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ILogger<MainViewModel> _logger;
        private readonly IConfiguration _config;
        private readonly ITransactionsService _service;

        public BudgetViewModel(ILogger<MainViewModel> logger, IConfiguration configuration, ITransactionsService service)
        {
            _logger = logger;
            _config = configuration;
            _service = service;
            _notNecessarilyList = new List<ExpenseCategory> {
                new() { Category = "cat1", Amount = 200},
                new() { Category = "cat2", Amount = 21},
                new() { Category = "cat2", Amount = 321},
                new() { Category = "cat2", Amount = 451}
            }.OrderByDescending(e => e.Amount).ToList();
            _necessarilyList = new List<ExpenseCategory> { new ExpenseCategory { Category = "cat3", Amount = 200 }, new ExpenseCategory { Category = "cat4", Amount = 2001 } };

            Years = new ObservableCollection<int>();
            for (int i = DateTime.Now.Year; i >= 2025; i--)
                Years.Add(i);
            
            Months = new ObservableCollection<string>(DateTimeFormatInfo.CurrentInfo.MonthNames.Take(12).ToArray());

            SelectedMonth = Months[DateTime.Now.Month - 1];
            SelectedYear = DateTime.Now.Year;
        }
    }
}
