namespace Finace.ViewModels
{
    public class SubCategoryViewModel : ViewModelBase
    {
        public string SubCategory { get; }
        public double Total { get; }

        public SubCategoryViewModel(string subCategory, double total)
        {
            SubCategory = subCategory;
            Total = total;
        }
    }
}
