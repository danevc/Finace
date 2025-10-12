using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Finace.ViewModels
{
    public interface IMainViewModel
    {
        ICommand SaveCommand { get; }
    }

    public class MainViewModel : IMainViewModel
    {
        public ICommand SaveCommand => throw new NotImplementedException();
    }
}
