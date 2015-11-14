using System;
using System.Diagnostics;
using Splat;

namespace Logger
{
    public class DefaultUserEnabledLogger : IUserEnabledLogger
    {
        public void Write(string message, LogLevel logLevel)
        {
            Debug.WriteLine($"Logger[{logLevel},{DateTime.Now.ToString("d")}]: " + message);
        }

        public LogLevel Level { get; set; }
        public void SetUser(string username)
        {
            Debug.WriteLine($"Username: {username}");
        }
    }
}
