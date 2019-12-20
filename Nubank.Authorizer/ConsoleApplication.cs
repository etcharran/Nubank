using Microsoft.Extensions.Hosting;
using Nubank.Contract;
using Nubank.Domain.Logic;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nubank.Authorizer
{
    public class ConsoleApplication : IHostedService
    {
        private readonly IOperationLogic operationLogic;
        private readonly IHostApplicationLifetime appLifetime;
        public ConsoleApplication(IOperationLogic operationLogic, IHostApplicationLifetime appLifetime)
        {
            this.operationLogic = operationLogic;
            this.appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var response = Task.CompletedTask;

            // Once the Application host has started, the process is launched.
            response.ContinueWith(DoWork())
            // Once the process has finished the application host has to be stopped.
            .ContinueWith((task) => appLifetime.StopApplication());

            return response;
        }

        private Action<Task> DoWork()
        {
            return (task) =>
            {
                string line;
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    IData data = JsonContractFactory.ToContract(line);

                    // Cada línea es una operación. Por lo tanto, opero
                    operationLogic.Operate(data);
                }

            };
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
