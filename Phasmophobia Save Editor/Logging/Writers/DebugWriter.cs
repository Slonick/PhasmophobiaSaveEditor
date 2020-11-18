using System.Diagnostics;

namespace PhasmophobiaSaveEditor.Logging.Writers
{
    public class DebugWriter : ILogWriter
    {
        public void Write(string line) => Debug.WriteLine(line);
    }
}