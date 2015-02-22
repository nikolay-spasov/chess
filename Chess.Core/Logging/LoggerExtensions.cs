namespace Chess.Core.Logging
{
    using System;

    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, string message)
        {
            var entry = new LogEntry(LoggingEventType.Information,
                message, null, null);

            logger.Log(entry);
        }

        public static void Log(this ILogger logger, Exception exception)
        {
            var entry = new LogEntry(LoggingEventType.Error,
                exception.Message, null, exception);

            logger.Log(entry);
        }
    }
}
