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

        public override bool IsValid(Transaction data)
        {
            var sameMerchantAmount = transactionRepository.GetAll()
                .Where(t => t.Merchant == data.Merchant && t.Amount == data.Amount)
                .Where(t => t.Time >= data.Time.AddMinutes(-2) && t.Time <= data.Time.AddMinutes(2)).ToList();

            sameMerchantAmount.Add(data);

            foreach (var transaction in sameMerchantAmount)
            {
                if (sameMerchantAmount.Where(t => t.Time <= transaction.Time.AddMinutes(2) && t.Time >= transaction.Time).Count() > 2)
                    return false;
            }

            return true;
        }


    }
}
