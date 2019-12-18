using System;

namespace Nubank.Authorizer.Operations
{
    public class OperationFactory: IOperationFactory
    {
        private readonly IServiceProvider serviceProvider;
        public OperationFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IOperation CreateOperation(string operationName)
        {
            switch (operationName)
            {
                case "account":
                    return (AccountCreation)serviceProvider.GetService(typeof(AccountCreation));
                case "transaction":
                    return (Transaction)serviceProvider.GetService(typeof(Transaction));
                default:
                    throw new Exception("Operation not recognized");
            }
        }
    }
}
