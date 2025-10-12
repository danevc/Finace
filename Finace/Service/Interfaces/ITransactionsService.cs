using System.Transactions;

namespace Finace.Service.Interfaces
{
    public interface ITransactionsService
    {
        /// <summary>
        /// Получение транзакции
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Transaction GetTransaction(long transactionId);

        /// <summary>
        /// Получение списка транзакций
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Transaction GetTransactions(int limit, int offset);

        /// <summary>
        /// Создание транзакции в БД
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Transaction CreateTransaction(Transaction transaction);

        /// <summary>
        /// Создание транзакции в БД
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Transaction UpdateTransaction(Transaction transaction);

        /// <summary>
        /// Удаление транзакции в БД
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool RemoveTransaction(long transactionId);
    }
}
