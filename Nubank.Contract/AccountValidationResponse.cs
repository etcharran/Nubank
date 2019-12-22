namespace Nubank.Contract
{
    public class AccountValidationResponse : IValidationResponse
    {
        public Account Account { get; set; }
        public string[] Violations { get; set; }
    }
}