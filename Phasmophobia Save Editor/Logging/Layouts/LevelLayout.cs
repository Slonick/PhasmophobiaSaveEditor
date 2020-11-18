using System.Text;

namespace PhasmophobiaSaveEditor.Logging.Layouts
{
    [LogLayout("level")]
    internal sealed class LevelLayout : ILogLayout
    {
        public void Append(StringBuilder builder, LogEventInfo logEvent, string layoutFormat)
        {
            builder.Append(logEvent.Level.ToString().ToUpperInvariant());
        }
    }
}