using Logger;
using Splat;
using System;
using System.Linq;

namespace Ets.Mobile.Logger
{
    public class CombinedLogger : IFullLogger, IUserEnabledLogger
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

        public void Debug<T>(T value)
        {
            Debug(value.ToString());
        }

        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            Debug(value.ToString());
        }

        public void DebugException(string message, Exception exception)
        {
            Debug(message + $", EXCEPTION: {ExceptionToString(exception)}");
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
            Debug(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Debug(string message)
        {
            Write(message, LogLevel.Debug);
        }

        public void Debug(string message, params object[] args)
        {
            Debug(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Debug<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            Debug(message, argument);
        }

        public void Debug<TArgument>(string message, TArgument argument)
        {
            Debug(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}");
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            Debug(message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            Debug(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}");
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            Debug(message, argument1, argument2, argument3);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
            TArgument3 argument3)
        {
            Debug(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}, TArgument3 is {typeof(TArgument3)}, ToString is {argument3}");
        }

        public void Info<T>(T value)
        {
            Info(value.ToString());
        }

        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            Info(value.ToString());
        }

        public void InfoException(string message, Exception exception)
        {
            Info(message + $", EXCEPTION: {ExceptionToString(exception)}");
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
            Info(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Info(string message)
        {
            Info(message, LogLevel.Info);
        }

        public void Info(string message, params object[] args)
        {
            Info(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Info<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            Info(message, argument);
        }

        public void Info<TArgument>(string message, TArgument argument)
        {
            Info(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}");
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            Info(message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            Info(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}");
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            Info(message, argument1, argument2, argument3);
        }

        public void Info<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
            TArgument3 argument3)
        {
            Info(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}, TArgument3 is {typeof(TArgument3)}, ToString is {argument3}");
        }

        public void Warn<T>(T value)
        {
            Warn(value.ToString());
        }

        public void Warn<T>(IFormatProvider formatProvider, T value)
        {
            Warn(value.ToString());
        }

        public void WarnException(string message, Exception exception)
        {
            Warn(message + $", EXCEPTION: {ExceptionToString(exception)}");
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
            Warn(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Warn(string message)
        {
            Warn(message, LogLevel.Warn);
        }

        public void Warn(string message, params object[] args)
        {
            Warn(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Warn<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            Warn(message, argument);
        }

        public void Warn<TArgument>(string message, TArgument argument)
        {
            Warn(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}");
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            Warn(message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            Warn(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}");
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            Warn(message, argument1, argument2, argument3);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
            TArgument3 argument3)
        {
            Warn(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}, TArgument3 is {typeof(TArgument3)}, ToString is {argument3}");
        }

        public void Error<T>(T value)
        {
            Error(value.ToString());
        }

        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            Error(value.ToString());
        }

        public void ErrorException(string message, Exception exception)
        {
            Error(message + $", EXCEPTION: {ExceptionToString(exception)}");
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
            Error(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Error(string message)
        {
            Error(message, LogLevel.Error);
        }

        public void Error(string message, params object[] args)
        {
            Error(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Error<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            Error(message, argument);
        }

        public void Error<TArgument>(string message, TArgument argument)
        {
            Error(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}");
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            Error(message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            Error(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}");
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            Error(message, argument1, argument2, argument3);
        }

        public void Error<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
            TArgument3 argument3)
        {
            Error(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}, TArgument3 is {typeof(TArgument3)}, ToString is {argument3}");
        }

        public void Fatal<T>(T value)
        {
            Fatal(value.ToString());
        }

        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            Fatal(value.ToString());
        }

        public void FatalException(string message, Exception exception)
        {
            Fatal(message + $", EXCEPTION: {ExceptionToString(exception)}");
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
            Fatal(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Fatal(string message)
        {
            Fatal(message, LogLevel.Fatal);
        }

        public void Fatal(string message, params object[] args)
        {
            Fatal(message + $", object[] : {string.Join(",", args.ToString())}");
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            Fatal(message, argument);
        }

        public void Fatal<TArgument>(string message, TArgument argument)
        {
            Fatal(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}");
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2)
        {
            Fatal(message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            Fatal(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}");
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
            TArgument2 argument2, TArgument3 argument3)
        {
            Fatal(message, argument1, argument2, argument3);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
            TArgument3 argument3)
        {
            Fatal(message + $", TArgument1 is {typeof(TArgument1)}, ToString is {argument1}, TArgument2 is {typeof(TArgument2)}, ToString is {argument2}, TArgument3 is {typeof(TArgument3)}, ToString is {argument3}");
        }

        private string ExceptionToString(Exception ex)
        {
            return InnerExceptionToString(ex.Message, ex.InnerException);
        }

        private static string InnerExceptionToString(string message, Exception inner)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(inner.Message))
                {
                    return message;
                }

                message += " -Inner-> " + inner.Message;
                inner = inner.InnerException;
            }
        }
    }
}