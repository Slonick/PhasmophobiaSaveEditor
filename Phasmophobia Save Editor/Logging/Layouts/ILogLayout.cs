﻿using System.Text;

namespace PhasmophobiaSaveEditor.Logging.Layouts
{
    public interface ILogLayout
    {
        void Append(StringBuilder builder, LogEventInfo logEvent, string layoutFormat);
    }
}