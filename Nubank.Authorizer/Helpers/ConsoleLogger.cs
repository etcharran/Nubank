using Microsoft.Extensions.Logging;
using System;

namespace Nubank.Authorizer.Helpers
{
    /// <summary>
    /// Console Logger (StdIn, StdOut)
    /// </summary>
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