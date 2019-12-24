using Nubank.Contract;
using System.Collections.Generic;

namespace Nubank.Persistence.Repositories
{
    public interface ITransactionRepository
    {
        void Create(Transaction data);
        IList<Transaction> GetAll();
    }
}