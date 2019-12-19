namespace Nubank.Authorizer.Operations
{
    public interface IOperationFactory
    {
        IOperation CreateOperation(string operationName);
    }
}
