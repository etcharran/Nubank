using System.Collections.Generic;
using Nubank.Contract;

namespace Nubank.Persistence.Repositories
{
    public interface ITransactionRepository
    {
        void Create(Transaction data);
        IList<Transaction> GetAll();
    }
}