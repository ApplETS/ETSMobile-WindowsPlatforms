using System;
using System.Collections.Generic;
using System.Text;
using Splat;

namespace StoreFramework.Logger
{
    public interface IUserEnabledLogger : ILogger
    {
        void SetUser(string username);
    }
}
