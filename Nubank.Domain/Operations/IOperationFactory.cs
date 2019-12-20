using Nubank.Contract;
using System;

namespace Nubank.Domain.Operations
{
    public interface IOperationFactory
    {
        IOperation CreateOperation(IData data);
    }
}
