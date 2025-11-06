namespace Finace.Options
{
    public class Settings
    {
        public string? Folder { get; set; }

        public List<string>? CategoriesIncome { get; set; }

        public List<string>? CategoriesNecessarily { get; set; }

        public List<string>? Tags { get; set; }

        public int? TotalBudgetNecessarily { get; set; }

        public int? TotalBudgetNotNecessarily { get; set; }
    }
}
