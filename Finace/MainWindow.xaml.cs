using Finace.Controls;
using Finace.ViewModels;
using Finace.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Finace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BudgetPage _budgetPage;
        private SettingsPage _settingsPage;
        private DashboardPage _dashboardPage;
        private readonly MenuButton[] _navButtons;

        public MainWindow(IMainViewModel viewModel,
            BudgetPage budgetPage,
            SettingsPage settingsPage,
            DashboardPage dashboardPage)
        {
            InitializeComponent();

            _budgetPage = budgetPage;
            _settingsPage = settingsPage;
            _dashboardPage = dashboardPage;
            _navButtons = [budgetPageButton, settingsButton, dashboardButton];

            DataContext = viewModel;
            SetFrame(_budgetPage, budgetPageButton);
        }

        private void BudgetPageClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_budgetPage, budgetPageButton);
        }
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_settingsPage, settingsButton);
        }

        private void DashboardClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_dashboardPage, dashboardButton);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RollUp(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove(); 
        }

        private void SetFrame(Page page, MenuButton activeButton)
        {
            MainFrame.Content = page;
            activeButton.IsActive = true;

            foreach (var btn in _navButtons)
            {
                if(btn != activeButton) btn.IsActive = false;
            }
        }
    }
}