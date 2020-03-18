using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nubank.Authorizer.Helpers;
using Nubank.Contract;
using Nubank.Domain.Operations;
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

        /// <summary>
        /// Initialize Host
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var response = Task.CompletedTask;

            // Once the Application host has started, the process is launched.
            response.ContinueWith(DoWork())
            // Once the process has finished the application host has to be stopped.
            .ContinueWith((task) => appLifetime.StopApplication());

            return response;
        }

        /// <summary>
        /// Work to do after Host is Initialized
        /// </summary>
        /// <returns></returns>
        private Action<Task> DoWork()
        {
            return (task) =>
            {
                string line;
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    try
                    {
                        Data data = JsonHelper.ToContract(line);

                        // Each line is an operation, hence we operate
                        var responseOperation = operationLogic.Process(data);

                        // Log response as string
                        logger.LogInformation(JsonHelper.Serialize(responseOperation));

                    }
                    catch (System.Exception ex)
                    {
                        logger.LogError(ex, ex.Message);
                    }
                }

            };
        }

        /// <summary>
        /// Task to Execute when stopping the host
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
