namespace Operations
{
    public interface IOperation
    {
        string Name { get; }

        void Process();
    }
}
