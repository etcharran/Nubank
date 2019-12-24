using Nubank.Contract;
using System.Collections.Generic;
using System.Linq;

namespace Nubank.Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        /// <summary>
        /// Transaction Storage
        /// </summary>
        private List<Transaction> Transactions { get; set; }

        public TransactionRepository()
        {
            Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Adds a Transaction to the storage
        /// </summary>
        /// <param name="data"></param>
        public void Create(Transaction data)
        {
            Transactions.Add(data.Clone());
        }

        /// <summary>
        /// Retrieves all Transactions
        /// </summary>
        /// <returns></returns>
        public IList<Transaction> GetAll()
        {
            return Transactions.Select(d => d.Clone()).ToList();
        }
    }
}