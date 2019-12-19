using System.Text.Json;

namespace Nubank.Domain.Logic
{
    public interface IOperationLogic
    {
        void Operate(JsonDocument operation);
    }
}