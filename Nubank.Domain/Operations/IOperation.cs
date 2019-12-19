namespace Nubank.Domain.Operations
{
    public interface IOperation
    {
        string Name { get; }

        void Process();
    }
}
