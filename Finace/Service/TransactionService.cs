using Finace.Service.Interfaces;
using Finace.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MainViewModel> _logger;
        private readonly IConfiguration _config;

        public TransactionService(ILogger<MainViewModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

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
