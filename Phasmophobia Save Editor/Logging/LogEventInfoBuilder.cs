using System;

namespace PhasmophobiaSaveEditor.Logging
{
    public static class LogEventInfoBuilder
    {
        public static LogEventInfo Create()
            => new LogEventInfo();

        public static LogEventInfo Create(LogLevel level, string message, object[] parameters, Exception exception = null)
            => new LogEventInfo(level, message, parameters, exception);

        public static LogEventInfo WithCategory(this LogEventInfo logEventInfo, string category)
        {
            logEventInfo.LoggerName = category;
            return logEventInfo;
        }

        public static LogEventInfo WithTimeStamp(this LogEventInfo logEventInfo, DateTimeOffset dateTime)
        {
            logEventInfo.TimeStamp = dateTime;
            return logEventInfo;
        }
    }
}