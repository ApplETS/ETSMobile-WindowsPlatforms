using Splat;
using System;

namespace Logger.SplatLog
{
    public class SplatLogger : ILogger
    {
        public LogLevel Level { get; set; }

        public void Write([Localizable(false)] string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    SplatEventSource.Log.Debug(message);
                    break;
                case LogLevel.Info:
                    SplatEventSource.Log.Info(message);
                    break;
                case LogLevel.Warn:
                    SplatEventSource.Log.Warn(message);
                    break;
                case LogLevel.Error:
                    SplatEventSource.Log.Error(message);
                    break;
                case LogLevel.Fatal:
                    SplatEventSource.Log.Error($"[Fatal Error] {message}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}