using Nubank.Contract;
using Nubank.Persistence.Repositories;
using System.Linq;

namespace Nubank.Domain.Validation
{
    public class HighFrequencyValidation : BusinessValidation<Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        public const string name = "high-frequency-small-interval";
        public override string ValidationName => name;

        public HighFrequencyValidation(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }


        public override bool IsValid(Transaction data)
        {
            var transactions = transactionRepository.GetAll()
                .Where(t => t.Time >= data.Time.AddMinutes(-2) && t.Time <= data.Time.AddMinutes(2)).ToList();

            transactions.Add(data);

            foreach (var transaction in transactions)
            {
                if (transactions.Where(t => t.Time <= transaction.Time.AddMinutes(2) && t.Time >= transaction.Time).Count() > 3)
                    return false;
            }

            return true;
        }
    }
}
