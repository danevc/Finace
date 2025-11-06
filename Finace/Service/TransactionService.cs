using Finace.Options;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.IO;

namespace Finace.Service
{
    public interface ITransactionsService { public List<Models.Transaction>? Transactions { get; set; } }

    public class TransactionService : ITransactionsService
    {
        public List<Models.Transaction>? Transactions { get; set; }
        private Settings _settings;

        public TransactionService(IOptions<Settings> configuration)
        {
            _settings = configuration.Value;
            FillTransactionsFromCSV();
        }

        private void FillTransactionsFromCSV()
        {
            string? filePath = Directory.GetFiles(_settings.Folder, "*.csv")
                .Where(f => Path.GetFileName(f).Contains("Money_Flow"))
                .OrderByDescending(f => File.GetCreationTime(f))
                .FirstOrDefault();

            Transactions = new List<Models.Transaction>();

            if (filePath != null)
            {
                using var sr = new StreamReader(filePath);
                var header = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split(',');
                    decimal parsedAmount = 0;
                    decimal? transferAmount = null;

                    DateTime parsedDate = DateTime.MinValue;
                    int id = 0;

                    int.TryParse(parts.ElementAtOrDefault(0), out id);
                    DateTime.TryParse(parts.ElementAtOrDefault(1), out parsedDate);
                    decimal.TryParse(parts.ElementAtOrDefault(3), NumberStyles.Any, CultureInfo.InvariantCulture, out parsedAmount);

                    var transferAmountStr = parts.ElementAtOrDefault(10);
                    if (!string.IsNullOrWhiteSpace(transferAmountStr))
                    {
                        if (decimal.TryParse(transferAmountStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var ta))
                            transferAmount = ta;
                    }

                    var rec = new Models.Transaction
                    {
                        Id = id,
                        Date = parsedDate,
                        Account = parts.ElementAtOrDefault(2),
                        Amount = parsedAmount,
                        Currency = parts.ElementAtOrDefault(4),

                        ParentCategory = parts.ElementAtOrDefault(5),
                        SubCategory = parts.ElementAtOrDefault(6),
                        Category = parts.ElementAtOrDefault(7),

                        Counterparty = parts.ElementAtOrDefault(8),

                        TransferAccount = parts.ElementAtOrDefault(9),
                        TransferAmount = transferAmount,
                        TransferCurrency = parts.ElementAtOrDefault(11),

                        Tags = parts.ElementAtOrDefault(12),
                        Location = parts.ElementAtOrDefault(13),
                        Note = parts.ElementAtOrDefault(14)
                    };

                    Transactions.Add(rec);
                }
            }
        }
    }
}
