namespace Nubank.Contract
{
    public class Account : Data, IClonable<Account>
    {
        public const string name = "account";

        public bool ActiveCard { get; set; }
        public int AvailableLimit { get; set; }

        public override string Name => name;

        public Account Clone()
        {
            return new Account { ActiveCard = ActiveCard, AvailableLimit = AvailableLimit };
        }

        public override string ToString() => $"ActiveCard: {ActiveCard}, AvailableLimit: {AvailableLimit}";
    }
}