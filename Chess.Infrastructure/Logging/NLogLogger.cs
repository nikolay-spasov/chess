namespace Chess.Infrastructure.Logging
{
    using System.Collections.Generic;
    using NLog;

    using Chess.Core.Logging;

    public class NLogLogger : ILogger
    {
        private Logger _logger;
        private Dictionary<LoggingEventType, LogLevel> _logLevelStrategy;

        public NLogLogger(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
            _logLevelStrategy = new Dictionary<LoggingEventType, LogLevel>()
            {
                {LoggingEventType.Debug, LogLevel.Debug},
                {LoggingEventType.Error, LogLevel.Error},
                {LoggingEventType.Fatal, LogLevel.Fatal},
                {LoggingEventType.Information, LogLevel.Info}
            };
        }

        public void Log(LogEntry entry)
        {
            _logger.Log(_logLevelStrategy[entry.Severity], entry.Message,
                entry.Exception);
        }
    }
}
