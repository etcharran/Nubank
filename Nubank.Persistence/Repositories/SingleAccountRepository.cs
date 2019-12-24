using Nubank.Contract;

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
            return Account != null ? Account.Clone() : null;
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
        /// Deletes the current account
        /// </summary>
        /// <param name="data"></param>
        public void Delete(Account data)
        {
            Account = null;
        }

        /// <summary>
        /// Updates the current account
        /// </summary>
        /// <param name="data"></param>
        public void Update(Account data)
        {
            Account = data.Clone();
        }
    }
}