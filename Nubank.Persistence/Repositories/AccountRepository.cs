using Nubank.Contract;

namespace Nubank.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        // It only has one account, beacuse the requirement only specifies one 
        private Account Account { get; set; }

        public Account Get()
        {
            return Account != null ? Account.Clone() : null;
        }

        public void Create(Account data)
        {
            Account = data.Clone();
        }

        public void Delete(Account data)
        {
            Account = null;
        }

        public void Update(Account data)
        {
            Account = data.Clone();
        }
    }
}