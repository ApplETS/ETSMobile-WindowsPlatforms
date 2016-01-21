using Splat;
using System;
using System.Diagnostics;

namespace Logger
{
    public class DefaultUserEnabledLogger : IUserEnabledLogger
    {
        public void Write(string message, LogLevel logLevel)
        {
#if DEBUG
            Debug.WriteLine($"Logger[{logLevel},{DateTime.Now.ToString("d")}]: " + message);
#endif
        }

        public LogLevel Level { get; set; }
        public void SetUser(string username)
        {
            Debug.WriteLine($"Username: {username}");
        }
    }
}