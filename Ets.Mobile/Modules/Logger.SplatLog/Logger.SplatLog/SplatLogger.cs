using Splat;
using System;

namespace Logger.SplatLog
{
    public class SplatLogManager : ILogManager
    {
        private readonly IFullLogger _fullLogger;

        public SplatLogManager(IFullLogger fullLogger)
        {
            _fullLogger = fullLogger;
        }

        public IFullLogger GetLogger(Type type)
        {
            return _fullLogger;
        }
    }

    public class SplatLogger : IFullLogger
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
            if (argument is LogLevel)
            {
                Write(message, LogLevel.Debug);
            }
            else
            {
                Write(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}", LogLevel.Debug);
            }
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
            if (argument is LogLevel)
            {
                Write(message, LogLevel.Info);
            }
            else
            {
                Write(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}", LogLevel.Info);
            }
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
            if (argument is LogLevel)
            {
                Write(message, LogLevel.Warn);
            }
            else
            {
                Write(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}", LogLevel.Warn);
            }
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
            if (argument is LogLevel)
            {
                Write(message, LogLevel.Error);
            }
            else
            {
                Write(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}", LogLevel.Error);
            }
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
            if (argument is LogLevel)
            {
                Write(message, LogLevel.Fatal);
            }
            else
            {
                Write(message + $", TArgument is {typeof(TArgument)}, ToString is {argument}", LogLevel.Fatal);
            }
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
                if (string.IsNullOrEmpty(inner?.Message))
                {
                    return message;
                }

                message += " -Inner-> " + inner.Message;
                inner = inner.InnerException;
            }
        }
    }
}