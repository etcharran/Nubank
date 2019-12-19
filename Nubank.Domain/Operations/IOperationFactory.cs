namespace Nubank.Domain.Operations
{
    public interface IOperationFactory
    {
        IOperation CreateOperation(string operationName);
    }
}
