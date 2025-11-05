using Finace.ViewModels;
using System.Windows.Controls;

namespace Finace.Views
{
    /// <summary>
    /// Логика взаимодействия для BudgetPage.xaml
    /// </summary>
    public partial class BudgetPage : Page
    {
        public BudgetPage(IBudgetViewModel budgetViewModel)
        {
            InitializeComponent();

            DataContext = budgetViewModel;
        }
    }
}
