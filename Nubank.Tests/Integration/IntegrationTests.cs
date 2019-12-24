using System;
using Microsoft.Extensions.Hosting;
using Nubank.Authorizer;

namespace Nubank.Tests.Integration
{
    public abstract class IntegrationTests
    {
        public readonly IServiceProvider serviceProvider;
        public IntegrationTests() {
            serviceProvider = Program.CreateHostBuilder(new string[0]).Build().Services;
        }
    }
}