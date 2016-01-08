using Splat;

namespace Logger
{
    public interface IUserEnabledLogger : ILogger
    {
        void SetUser(string username);
    }
}