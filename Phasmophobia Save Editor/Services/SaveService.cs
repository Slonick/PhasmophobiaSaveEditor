using System;
using System.IO;
using System.Linq;
using System.Text;
using PhasmophobiaSaveEditor.Models;
using PhasmophobiaSaveEditor.Utils;

namespace PhasmophobiaSaveEditor.Services
{
    internal class SaveService
    {
        private const string Key = "CHANGE ME TO YOUR OWN RANDOM STRING";

        public SaveService(string filename)
        {
            this.Filename = filename;
        }

        public string Filename { get; set; }

        private static string ScrambleData(string data) => new string(data.Select((x, i) => Convert.ToChar(x ^ Key[i % Key.Length])).ToArray());

        public PhasmophobiaSave Read()
        {
            var data = File.ReadAllText(this.Filename);
            var content = ScrambleData(data);
            return JsonSerializer.Deserialize<PhasmophobiaSave>(content);
        }

        public void Save(PhasmophobiaSave save)
        {
            var data = JsonSerializer.Serialize(save);
            var content = ScrambleData(data);
            File.WriteAllText(this.Filename, content, Encoding.UTF8);
        }

        public bool TryRead(out PhasmophobiaSave save)
        {
            try
            {
                save = this.Read();
                return true;
            }
            catch
            {
                save = null;
                return false;
            }
        }

        public string ReadRaw()
        {
            var data = File.ReadAllText(this.Filename);
            return JsonSerializer.FormatJson(ScrambleData(data));
        }
    }
}