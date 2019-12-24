using Nubank.Contract;
using Nubank.Persistence.Repositories;
using System.Linq;

namespace Nubank.Domain.Validation
{
    public class DoubledTransactionValidation : BusinessValidation<Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        public override string ValidationName => "doubled-transaction";

        public DoubledTransactionValidation(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }


        public override bool IsValid(Transaction data) =>
            transactionRepository.GetAll().Where(t => !(t.Merchant == data.Merchant && t.Amount == data.Amount && t.Time > data.Time.AddMinutes(-2))).Count() <= 2;
    }
}
