using Finace.ViewModels;
using System.Windows.Controls;

namespace Finace.Views
{
    /// <summary>
    /// Логика взаимодействия для BudgetPage.xaml
    /// </summary>
    public partial class BudgetPage : Page
    {
        IBudgetViewModel _budgetViewModel;
        public BudgetPage(IBudgetViewModel budgetViewModel)
        {
            InitializeComponent();

            DataContext = budgetViewModel;
            _budgetViewModel = budgetViewModel;
        }

        private void YearMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _budgetViewModel.DatesChanged();
        }
    }
}
