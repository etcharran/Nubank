using System.Collections.Generic;
using System.Linq;
using Nubank.Contract;

namespace Nubank.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private List<Transaction> Transactions { get; set; }
        public void Create(Transaction data)
        {
            Transactions.Add(data.Clone());
        }

        public IList<Transaction> GetAll()
        {
            return Transactions.Select(d => d.Clone()).ToList();
        }
    }
}