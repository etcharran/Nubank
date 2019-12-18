using Microsoft.Extensions.Hosting;
using Nubank.Authorizer.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Nubank.Authorizer
{
    public class ConsoleApplication : IHostedService
    {
        private readonly IOperationLogic operationLogic;
        public ConsoleApplication(IOperationLogic operationLogic)
        {
            this.operationLogic = operationLogic;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            string line;
            while(!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                // Parseo el documento que me llega como string a json
                var document = JsonDocument.Parse(line);

                // Cada línea es una operación. Por lo tanto, opero
                operationLogic.Operate(document);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
