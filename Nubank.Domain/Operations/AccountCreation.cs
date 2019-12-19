namespace Nubank.Domain.Operations
{
    public class AccountCreation : IOperation
    {
        public string Name => "account";

        private bool ActiveCard { get; set; }
        private int AvailableLimit { get; set; }

        public void Process()
        {
            // throw new System.NotImplementedException();
        }
    }
}