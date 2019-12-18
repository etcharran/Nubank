using System;

namespace Nubank.Authorizer.Operations
{
    public class Transaction : IOperation
    {
        public string Name => "transaction";

        private string Merchant { get; set; }
        private int Amount { get; set; }
        private DateTime Time { get; set; }

        public void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}