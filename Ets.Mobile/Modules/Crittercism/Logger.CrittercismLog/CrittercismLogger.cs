using System;
using CrittercismSDK;
using Splat;

namespace Logger.CrittercismLog
{
    public sealed class CrittercismLogger : IUserEnabledLogger
    {
        public void Write(string message, LogLevel logLevel)
        {
            Crittercism.LogHandledException(new Exception($"[{DateTime.Now}] {message} ({logLevel})"));
        }

        public LogLevel Level { get; set; }
        public void SetUser(string username)
        {
            Crittercism.SetUsername(username);
        }
    }
}
