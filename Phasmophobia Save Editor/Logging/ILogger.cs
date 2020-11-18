using System;

namespace PhasmophobiaSaveEditor.Logging
{
    public interface ILogger
    {
        void Debug(Exception exception, string message, params object[] argument);

        void Debug(string message, params object[] argument);

        void Error(Exception exception, string message = null, params object[] argument);

        void Error(string message, params object[] argument);

        void Fatal(Exception exception, string message = null, params object[] argument);

        void Fatal(string message, params object[] argument);

        void Info(Exception exception, string message, params object[] argument);

        void Info(string message, params object[] argument);

        void Log(LogLevel logLevel, Exception exception, string message, params object[] argument);

        void Log(LogLevel logLevel, string message, params object[] argument);

        void Trace(Exception exception, string message, params object[] argument);

        void Trace(string message, params object[] argument);

        void Warn(Exception exception, string message, params object[] argument);

        void Warn(string message, params object[] argument);
    }
}