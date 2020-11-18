using System.Collections.Generic;
using PhasmophobiaSaveEditor.Logging.Layouts;

namespace PhasmophobiaSaveEditor.Logging
{
    public interface ILogFactory
    {
        LoggerConfig Config { get; set; }

        ILogger GetLogger(string name);

        IDictionary<string, ILogLayout> GetLogLayouts();
    }
}