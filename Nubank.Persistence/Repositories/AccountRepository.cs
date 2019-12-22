using Nubank.Contract;

namespace Nubank.Persistence.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        // It only has one account, beacuse the requirement only specifies one 
        private Account Account { get; set; }

        public Account Get()
        {
            return Account != null ? Account.Copy() : null;
        }
        
        public void Create(Account data)
        {
            Account = data;
        }

        public void Delete(Account data)
        {
            Account = null;
        }

        public void Update(Account data)
        {
            Account = data;
        }
    }
}