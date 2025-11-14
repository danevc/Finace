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
        public double Total { get; set; }

        public ICommand ToggleExpandCommand { get; }

        public ParentCategoryViewModel(string parent, IEnumerable<SubCategoryViewModel> subs)
        {
            if (subs.Count() == 1 && subs.First().SubCategory == parent)
            {
                Subcategories = new ObservableCollection<SubCategoryViewModel>();
                Total = subs.First().Total;
            }
            else
            {
                Subcategories = new ObservableCollection<SubCategoryViewModel>(subs.OrderByDescending(e => e.Total));
                Total = Subcategories.Sum(s => s.Total);
            }

            ParentCategory = parent;
            ToggleExpandCommand = new RelayCommand(() => IsExpanded = !IsExpanded);

        }
    }
}
