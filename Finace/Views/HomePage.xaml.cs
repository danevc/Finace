using Finace.Controls;
using Finace.ViewModels;
using Finace.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Finace.Views
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly TabButton[] _navButtons;

        CostPage _costPage;
        IncomePage _incomePage;
        TransferPage _transferPage;

        public HomePage(IHomeViewModel viewModel,
            CostPage costPage,
            IncomePage incomePage,
            TransferPage transferPage)
        {
            InitializeComponent();
            DataContext = viewModel;
            _navButtons = new[] { costButton, incomeButton, transferButton };

            _costPage = costPage;
            _incomePage = incomePage;
            _transferPage = transferPage;
            SetFrame(_costPage, costButton);
        }

        private void CostClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_costPage, costButton);
        }
        private void IncomeClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_incomePage, incomeButton);
        }

        private void TransferClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_transferPage, transferButton);
        }

        private void SetFrame(Page page, TabButton activeButton)
        {
            MainFrame.Content = page;
            activeButton.IsActive = true;

            foreach (var btn in _navButtons)
            {
                if (btn != activeButton) btn.IsActive = false;
            }
        }
    }
}
