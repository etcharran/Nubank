using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nubank.Authorizer.Helpers;
using Nubank.Contract;
using Nubank.Domain.Logic;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nubank.Authorizer
{
    public class ConsoleApplication : IHostedService
    {
        private readonly IOperationLogic operationLogic;
        private readonly IHostApplicationLifetime appLifetime;
        private readonly ILogger logger;
        public ConsoleApplication(IOperationLogic operationLogic, IHostApplicationLifetime appLifetime, ILogger logger)
        {
            this.operationLogic = operationLogic;
            this.appLifetime = appLifetime;
            this.logger = logger;
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
                try
                {
                    string line;
                    while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                    {
                        IData data = JsonHelper.ToContract(line);

                        // Each line is an operation, hence we operate
                        var responseOperation = operationLogic.Operate(data);

                        // Log response as string
                        logger.LogInformation(JsonHelper.Serialize(responseOperation));

                    }
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }

            };
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
