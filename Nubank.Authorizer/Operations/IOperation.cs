namespace Nubank.Authorizer.Operations
{
    public interface IOperation
    {
        string Name { get; }

        void Process();
    }
}
