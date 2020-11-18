using System;
using System.Text;

namespace PhasmophobiaSaveEditor.Logging.Layouts
{
    [LogLayout("memory")]
    internal sealed class TotalMemoryInfoLayout : ILogLayout
    {
        public void Append(StringBuilder builder, LogEventInfo logEvent, string layoutFormat)
        {
            builder.Append(GC.GetTotalMemory(false));
        }
    }
}