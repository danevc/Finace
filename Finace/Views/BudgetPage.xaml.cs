using Finace.ViewModels;
using System.Windows;
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
            _budgetViewModel.UpdatePage();
        }

        private void includeTags_Checked(object sender, RoutedEventArgs e)
        {
            _budgetViewModel.UpdatePage();
        }

        private void includeTags_Unchecked(object sender, RoutedEventArgs e)
        {
            _budgetViewModel.UpdatePage();
        }
    }
}
