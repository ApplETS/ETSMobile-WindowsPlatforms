﻿using CrittercismSDK;
using Splat;
using System;

namespace Logger.CrittercismLog
{
    public sealed class CrittercismLogger : ILogger, IUserEnabledLogger
    {
        public void Write(string message, LogLevel logLevel)
        {
            if (logLevel == LogLevel.Error)
            {
                Crittercism.LogHandledException(new Exception($"[{DateTime.Now}] {message} ({logLevel})"));
            }
            else if (logLevel == LogLevel.Fatal)
            {
                Crittercism.LogUnhandledException(new Exception($"[{DateTime.Now}] {message} ({logLevel})"));
            }
        }

        public LogLevel Level { get; set; }
        public void SetUser(string username)
        {
            Crittercism.SetUsername(username);
        }
    }
}