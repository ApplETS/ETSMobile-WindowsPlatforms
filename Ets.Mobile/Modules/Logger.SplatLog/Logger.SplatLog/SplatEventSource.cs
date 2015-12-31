using System.Diagnostics.Tracing;

namespace Logger.SplatLog
{
    public class SplatEventSource : EventSource
    {
        public static readonly SplatEventSource Log = new SplatEventSource();

        [Event(1, Level = EventLevel.Verbose)]
        public void Debug(string message)
        {
            WriteEvent(1, message);
        }

        [Event(2, Level = EventLevel.Informational)]
        public void Info(string message)
        {
            WriteEvent(2, message);
        }

        [Event(3, Level = EventLevel.Warning)]
        public void Warn(string message)
        {
            WriteEvent(3, message);
        }

        [Event(4, Level = EventLevel.Error)]
        public void Error(string message)
        {
            WriteEvent(4, message);
        }

        [Event(5, Level = EventLevel.Critical)]
        public void Critical(string message)
        {
            WriteEvent(5, message);
        }
    }
}