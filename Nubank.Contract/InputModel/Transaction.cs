using System;

namespace Nubank.Contract
{
    public class Transaction : Data, IClonable<Transaction>
    {
        public const string name = "transaction";

        public string Merchant { get; set; }
        public int Amount { get; set; }
        public DateTime Time { get; set; }

        public override string Name => name;

        public Transaction Clone()
        {
            return new Transaction { Amount = Amount, Merchant = Merchant, Time = Time };
        }

        public override string ToString() => $"Name: {Name}, Amount: {Amount}, Merchant: {Merchant}, Time: {Time.ToString("dd-MM-yyyy")}";

    }
}