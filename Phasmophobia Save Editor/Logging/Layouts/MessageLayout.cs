using System.Text;

namespace PhasmophobiaSaveEditor.Logging.Layouts
{
    [LogLayout("message")]
    internal sealed class MessageLayout : ILogLayout
    {
        public void Append(StringBuilder builder, LogEventInfo logEvent, string layoutFormat)
        {
            if (!string.IsNullOrWhiteSpace(logEvent.Message))
            {
                builder.Append(string.Format(logEvent.Message, logEvent.Parameters));
            }

            if (logEvent.Exception == null)
            {
                return;
            }

            builder.AppendLine(logEvent.Exception.Message);
            builder.AppendLine(logEvent.Exception.StackTrace);
        }
    }
}