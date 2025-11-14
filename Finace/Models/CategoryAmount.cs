namespace Finace.Models
{
    public class CategoryAmount
    {
        public string? ParentCategory { get; set; }
        
        public string? Category { get; set; }

        public double Amount { get; set; }
    }
}
