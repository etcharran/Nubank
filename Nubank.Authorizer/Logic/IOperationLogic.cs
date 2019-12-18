using System.Text.Json;

namespace Nubank.Authorizer.Logic
{
    public interface IOperationLogic
    {
        void Operate(JsonDocument operation);
    }
}