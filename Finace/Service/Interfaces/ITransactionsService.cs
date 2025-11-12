namespace Finace.Service.Interfaces
{
    public interface ITransactionsService
    {
        public List<Models.Transaction>? Transactions { get; set; }
        public DateTime? FirstTransactionDate { get; set; }
    }
}
