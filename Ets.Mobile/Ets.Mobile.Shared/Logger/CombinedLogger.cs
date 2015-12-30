using System;
using System.Linq;
using Logger;
using Splat;

namespace Ets.Mobile.Logger
{
    public class CombinedLogger : IUserEnabledLogger
    {
        private readonly ILogger[] _loggers;
        public CombinedLogger(params ILogger[] loggers)
        {
            if (loggers == null || loggers.Length == 0)
            {
                throw new ArgumentException("Insufficient amount of loggers");   
            }
            _loggers = loggers;
        }

        public void Write(string message, LogLevel logLevel)
        {
            foreach (var logger in _loggers)
            {
                logger.Write(message, logLevel);
            }
        }

        public LogLevel Level { get; set; }
        public void SetUser(string username)
        {
            foreach (var logger in _loggers.OfType<IUserEnabledLogger>())
            {
                logger.SetUser(username);
            }
        }
    }
}
