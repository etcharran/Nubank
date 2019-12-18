using System;
using System.Collections.Generic;
using System.Text;

namespace Nubank.Authorizer.Operations
{
    public interface IOperationFactory
    {
        IOperation CreateOperation(string operationName);
    }
}
