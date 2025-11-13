using Finace.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Finace.ViewModels
{
    public class ParentCategoryViewModel : ViewModelBase
    {
        public string ParentCategory { get; }
        public ObservableCollection<SubCategoryViewModel> Subcategories { get; }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set { _isExpanded = value; Raise(); }
        }

        // Сумма всех подкатегорий
        public double Total => Subcategories.Sum(s => s.Total);

        public ICommand ToggleExpandCommand { get; }

        public ParentCategoryViewModel(string parent, IEnumerable<SubCategoryViewModel> subs)
        {
            ParentCategory = parent;
            Subcategories = new ObservableCollection<SubCategoryViewModel>(subs.OrderByDescending(e => e.Total));
            ToggleExpandCommand = new RelayCommand(() => IsExpanded = !IsExpanded);
        }
    }
}
