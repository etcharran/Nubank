namespace Nubank.Contract
{
    public class Transaction: Data
    {
        public const string name = "transaction";

        public string Merchant { get; set; }
        public int Amount { get; set; }

        public override string Name => name;
    }
}