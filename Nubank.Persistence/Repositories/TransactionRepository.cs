using System.Collections.Generic;
using Nubank.Contract;

namespace Nubank.Persistence.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private List<Transaction> Transactions { get; set; }
        public void Create(Transaction data)
        {
            Transactions.Add(data);
        }

        public void Delete(Transaction data)
        {
            throw new System.NotImplementedException();
        }

        public Transaction Get()
        {
            throw new System.NotImplementedException();
        }

        public Transaction[] GetAll()
        {
            return Transactions.ToArray();
        }

        public void Update(Transaction data)
        {
            throw new System.NotImplementedException();
        }
    }
}