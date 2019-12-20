using System;
using Microsoft.Extensions.Logging;

namespace Nubank.Authorizer.Helpers
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger() { }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"{formatter(state, exception)}");
        }

        IDisposable ILogger.BeginScope<TState>(TState state) => null;
    }
}