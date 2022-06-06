using System;

namespace WebApiSecurityDemo.Utils
{
    public interface ILoggerService
    {
        void LogWarn(string message);

        void LogError(Exception exception);

        void LogInfo(string message);
    }
}