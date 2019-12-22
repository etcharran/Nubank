namespace Nubank.Contract
{
    public class Account : Data
    {
        public const string name = "account";

        public bool ActiveCard { get; set; }
        public int AvailableLimit { get; set; }

        public override string Name => name;

        public Account Copy()
        {
            return new Account { ActiveCard = ActiveCard, AvailableLimit = AvailableLimit };
        }
    }
}