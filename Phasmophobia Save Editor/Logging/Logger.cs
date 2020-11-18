using System;
using System.Text;

namespace PhasmophobiaSaveEditor.Logging
{
    public class Logger : ILogger
    {
        internal static readonly Type DefaultLoggerType = typeof(Logger);

        private ILogFactory logFactory;

        public string Name { get; private set; }

        public void Debug(Exception exception, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Debug, message, argument, exception).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Debug(string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Debug, message, argument).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Error(Exception exception, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Error, message, argument, exception).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Error(string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Error, message, argument).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Fatal(Exception exception, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Fatal, message, argument, exception).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Fatal(string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Fatal, message, argument).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Info(Exception exception, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Info, message, argument, exception).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Info(string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Info, message, argument).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Initialize(string name, ILogFactory logFactoryInstance)
        {
            this.Name = name;
            this.logFactory = logFactoryInstance;
        }

        public void Log(LogLevel logLevel, Exception exception, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(logLevel, message, argument, exception).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Log(LogLevel logLevel, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(logLevel, message, argument).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Trace(Exception exception, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Trace, message, argument, exception).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Trace(string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Trace, message, argument).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Warn(Exception exception, string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Warn, message, argument, exception).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        public void Warn(string message, params object[] argument)
        {
            this.Write(LogEventInfoBuilder.Create(LogLevel.Warn, message, argument).WithCategory(this.Name).WithTimeStamp(DateTimeOffset.Now));
        }

        private string BuildLayout(LogEventInfo logEventInfo)
        {
            const string startSeparator = "${";
            const string endSeparator = "}";

            var result = new StringBuilder();
            var logLayout = this.logFactory.GetLogLayouts();
            var layout = this.logFactory.Config.Layout;
            var startIndex = layout.IndexOf(startSeparator, StringComparison.Ordinal);
            var endIndex = layout.IndexOf(endSeparator, StringComparison.Ordinal);
            while (startIndex > 0 && endIndex > 0)
            {
                result.Append(layout.Substring(0, startIndex));

                var layoutVariable = layout.Substring(startIndex, endIndex - startIndex + 1);
                var layoutArgs = layoutVariable.TrimStart(startSeparator.ToCharArray()).TrimEnd(endSeparator.ToCharArray()).Split(':');
                var layoutName = layoutArgs[0];
                var layoutFormat = layoutArgs.Length > 1 ? layoutArgs[1] : string.Empty;

                if (logLayout.ContainsKey(layoutName))
                {
                    logLayout[layoutName].Append(result, logEventInfo, layoutFormat);
                }
                else
                {
                    result.Append(layoutVariable);
                }

                layout = layout.Substring(endIndex + 1);
                startIndex = layout.IndexOf(startSeparator, StringComparison.Ordinal);
                endIndex = layout.IndexOf(endSeparator, StringComparison.Ordinal);
            }

            return result.ToString();
        }

        private void Write(LogEventInfo logEventInfo)
        {
            if (logEventInfo.Level < this.logFactory.Config.MinLogLevel)
            {
                return;
            }

            var line = this.BuildLayout(logEventInfo);


            foreach (var logWriter in this.logFactory.Config.LogWriters)
            {
                logWriter.Write(line);
            }
        }
    }
}