using Finace.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Finace.Service
{
    public class TransactionService : ITransactionsService
    {

        public Transaction CreateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransaction(long transactionId)
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransactions(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public bool RemoveTransaction(long transactionId)
        {
            throw new NotImplementedException();
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
