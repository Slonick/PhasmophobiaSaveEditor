using System.IO;

namespace PhasmophobiaSaveEditor.Logging.Writers
{
    public class FileWriter : ILogWriter
    {
        public FileWriter(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; }

        public void Write(string line)
        {
            using (var file = File.AppendText(this.FilePath))
            {
                file.WriteLine(line);
            }
        }
    }
}