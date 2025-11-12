using Finace.Controls;
using Finace.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Finace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BudgetPage _budgetPage;
        private DashboardPage _dashboardPage;
        private readonly MenuButton[] _navButtons;

        public MainWindow(BudgetPage budgetPage, DashboardPage dashboardPage)
        {
            InitializeComponent();

            _budgetPage = budgetPage;
            _dashboardPage = dashboardPage;
            _navButtons = [budgetPageButton, dashboardButton];
            SetFrame(_budgetPage, budgetPageButton);
        }

        private void BudgetPageClick(object sender, RoutedEventArgs e)
        {
            SetFrame(_budgetPage, budgetPageButton);
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FocusCatcher.Focus();
        }

        private void ThumbRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newWidth = Math.Max(this.MinWidth, this.Width + e.HorizontalChange);
            this.Width = newWidth;
        }

        private void ThumbLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;
            if (newWidth >= this.MinWidth)
            {
                this.Left += e.HorizontalChange;
                this.Width = newWidth;
            }
            else
            {
                double diff = this.Width - this.MinWidth;
                this.Left += diff;
                this.Width = this.MinWidth;
            }
        }

        private void ThumbBottom_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = Math.Max(this.MinHeight, this.Height + e.VerticalChange);
            this.Height = newHeight;
        }

        private void ThumbTop_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = this.Height - e.VerticalChange;
            if (newHeight >= this.MinHeight)
            {
                this.Top += e.VerticalChange;
                this.Height = newHeight;
            }
            else
            {
                double diff = this.Height - this.MinHeight;
                this.Top += diff;
                this.Height = this.MinHeight;
            }
        }

        private void ThumbTopLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbLeft_DragDelta(sender, e);
            ThumbTop_DragDelta(sender, e);
        }

        private void ThumbTopRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbTop_DragDelta(sender, e);
            ThumbRight_DragDelta(sender, e);
        }

        private void ThumbBottomLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbLeft_DragDelta(sender, e);
            ThumbBottom_DragDelta(sender, e);
        }

        private void ThumbBottomRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbRight_DragDelta(sender, e);
            ThumbBottom_DragDelta(sender, e);
        }
    }
}