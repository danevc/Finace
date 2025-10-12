using Finace.ViewModels;
using Finace.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Finace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HomeClick(object sender, EventArgs e)
        {
            MainFrame.Content = new HomePage();
        }
        private void SettingsClick(object sender, EventArgs e)
        {
            MainFrame.Content = new SettingsPage();
        }

        private void DashboardClick(object sender, EventArgs e)
        {
            MainFrame.Content = new DashboardPage();
        }
    }
}