using Finace.ViewModels;
using Finace.Views;
using System.Windows;

namespace Finace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HomePage _homePage;
        private SettingsPage _settingsPage;
        private DashboardPage _dashboardPage;

        public MainWindow(IMainViewModel viewModel,
            HomePage homePage,
            SettingsPage settingsPage,
            DashboardPage dashboardPage)
        {
            InitializeComponent();

            _homePage = homePage;
            _settingsPage = settingsPage;
            _dashboardPage = dashboardPage;

            DataContext = viewModel;
            MainFrame.Content = _homePage;
        }

        private void HomeClick(object sender, EventArgs e)
        {
            MainFrame.Content = _homePage;
        }
        private void SettingsClick(object sender, EventArgs e)
        {
            MainFrame.Content = _settingsPage;
        }

        private void DashboardClick(object sender, EventArgs e)
        {
            MainFrame.Content = _dashboardPage;
        }
    }
}