using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Finace.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void HomeClick(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }
        private void SettingsClick(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }

        private void DashboardClick(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }
    }
}
