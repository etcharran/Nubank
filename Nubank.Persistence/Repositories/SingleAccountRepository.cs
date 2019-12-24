using Nubank.Contract;
using Nubank.Tools.Exceptions;

namespace Nubank.Persistence.Repositories
{
    /// <summary>
    /// Manages only one account
    /// </summary>
    public class SingleAccountRepository : IAccountRepository
    {
        private Account Account { get; set; }

        /// <summary>
        /// Get the current Account
        /// </summary>
        /// <returns></returns>
        public Account Get()
        {
            return Account != null ? Account.Clone() : throw new NullAccountException();
        }

        /// <summary>
        /// Creates an account
        /// </summary>
        /// <param name="data"></param>
        public void Create(Account data)
        {
            Account = data.Clone();
        }

        /// <summary>
        /// Updates the current account
        /// </summary>
        /// <param name="data"></param>
        public void Update(Account data)
        {
            Account = data.Clone();
        }

        public bool Any() => Account != null;
    }
}