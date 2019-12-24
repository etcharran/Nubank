using Nubank.Contract;
using Nubank.Persistence.Repositories;
using System.Linq;

namespace Nubank.Domain.Validation
{
    public class HighFrequencyValidation : BusinessValidation<Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        public override string ValidationName => "high-frequency-small-interval";

        public HighFrequencyValidation(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public override bool IsValid(Transaction data) => transactionRepository.GetAll().Where(t => t.Time > data.Time.AddMinutes(-2)).Count() <= 2;
    }
}
