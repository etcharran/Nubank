using Nubank.Contract;
using System.Collections.Generic;

namespace Nubank.Persistence.Repositories
{
    /// <summary>
    /// Manages transactions
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Create a transaction
        /// </summary>
        /// <param name="data"></param>
        void Create(Transaction data);

        /// <summary>
        /// Retrieves all transactions
        /// </summary>
        /// <returns></returns>
        IList<Transaction> GetAll();
    }
}