using Finace.Controls;
using Finace.ViewModels;
using Finace.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private readonly MenuButton[] _navButtons;

        public MainWindow(IMainViewModel viewModel,
            HomePage homePage,
            SettingsPage settingsPage,
            DashboardPage dashboardPage)
        {
            InitializeComponent();

            _homePage = homePage;
            _settingsPage = settingsPage;
            _dashboardPage = dashboardPage;
            _navButtons = new[] { plusButton, settingsButton, dashboardButton };

            DataContext = viewModel;
            MainFrame.Content = _homePage;
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_homePage, plusButton);
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