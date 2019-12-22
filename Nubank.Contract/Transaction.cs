namespace Nubank.Contract
{
    public class Transaction: Data, ICLonable<Transaction>
    {
        public const string name = "transaction";

        public string Merchant { get; set; }
        public int Amount { get; set; }

        public override string Name => name;

        public Transaction Clone()
        {
            return new Transaction { Amount = Amount, Merchant = Merchant};
        }
    }
}