using System.Collections.Generic;

namespace Nubank.Contract
{
    public class AccountResponse : IResponse<Account>
    {
        public Account Account { get; set; }
        public IList<string> Violations { get; set; }

        public object ToResposeFormat()
        {
            return new { Account = new { Account.ActiveCard, Account.AvailableLimit }, Violations };
        }
    }
}
