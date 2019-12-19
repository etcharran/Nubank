using Microsoft.Extensions.Hosting;
using Nubank.Authorizer.Logic;
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
            response.ContinueWith((task) =>
            {
                string line;
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    // Parseo el documento que me llega como string a json
                    var document = JsonDocument.Parse(line);

                    // Cada línea es una operación. Por lo tanto, opero
                    operationLogic.Operate(document);
                }

            }).ContinueWith((task) => appLifetime.StopApplication());

            return response;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
