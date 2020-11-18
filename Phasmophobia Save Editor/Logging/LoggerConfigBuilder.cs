using System.Collections.Generic;
using PhasmophobiaSaveEditor.Logging.Writers;

namespace PhasmophobiaSaveEditor.Logging
{
    public static class LoggerConfigBuilder
    {
        public static LoggerConfig Create() => new LoggerConfig();

        public static LoggerConfig Create(bool includeLogOriginDetails, LogLevel minLogLevel) => new LoggerConfig(includeLogOriginDetails, minLogLevel);

        public static LoggerConfig UseDebugLogger(this LoggerConfig loggerConfig)
        {
            if (loggerConfig.LogWriters == null)
            {
                loggerConfig.LogWriters = new List<ILogWriter>();
            }

            loggerConfig.LogWriters.Add(new DebugWriter());
            return loggerConfig;
        }

        public static LoggerConfig UseFileLogger(this LoggerConfig loggerConfig, string filepath)
        {
            if (loggerConfig.LogWriters == null)
            {
                loggerConfig.LogWriters = new List<ILogWriter>();
            }

            loggerConfig.LogWriters.Add(new FileWriter(filepath));
            return loggerConfig;
        }

        public static LoggerConfig WithLayout(this LoggerConfig loggerConfig, string layout)
        {
            loggerConfig.Layout = layout;
            return loggerConfig;
        }
    }
}