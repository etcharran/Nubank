namespace Nubank.Contract
{
    interface IValidationResponse
    {
        string[] Violations { get; set; }
    }
}