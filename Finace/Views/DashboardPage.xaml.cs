using Finace.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Finace.Views
{
    /// <summary>
    /// Логика взаимодействия для DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        IDashboardViewModel _dashboardViewModel;
        public DashboardPage(IDashboardViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _dashboardViewModel = viewModel;
            DateTimePick.Visibility = Visibility.Collapsed;
        }

        private void AnyDate_Checked(object sender, RoutedEventArgs e)
        {
            DateTimePick.Visibility = Visibility.Visible;
            YearMonthPick.Visibility = Visibility.Collapsed;

            _dashboardViewModel.UpdatePage();
        }

        private void AnyDate_Unchecked(object sender, RoutedEventArgs e)
        {
            DateTimePick.Visibility = Visibility.Collapsed;
            YearMonthPick.Visibility = Visibility.Visible;

            _dashboardViewModel.UpdatePage();
        }

        private void YearMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _dashboardViewModel.UpdatePage();
        }

        private void DateTimePick_To_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            _dashboardViewModel.UpdatePage();
        }

        private void includeTags_TopCategories_Unchecked(object sender, RoutedEventArgs e)
        {
            _dashboardViewModel.UpdateCategories();
        }

        private void includeTags_TopCategories_Checked(object sender, RoutedEventArgs e)
        {
            _dashboardViewModel.UpdateCategories();
        }

        private void Unit_Average_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _dashboardViewModel.UpdateCategories();
        }

        private void TextBox_DataContextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                Convert.ToDouble(textBox.Text);
                _dashboardViewModel.UpdateCategories();
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный ввод");
            }
        }
    }
}
