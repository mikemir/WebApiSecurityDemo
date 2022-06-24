using log4net;
using System;

namespace WebApiSecurityDemo.Utils
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILog _logger;

        public LoggerManager(ILog logger)
        {
            _logger = logger;
        }

        public void LogError(Exception exception)
        {
            _logger.Error("", exception);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}