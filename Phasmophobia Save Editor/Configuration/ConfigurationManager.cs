using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using PhasmophobiaSaveEditor.Configuration.Attributes;
using PhasmophobiaSaveEditor.Logging;
using PhasmophobiaSaveEditor.Utils;

namespace PhasmophobiaSaveEditor.Configuration
{
    public class ConfigurationManager<T> : IConfigurationManager<T> where T : new()
    {
        private readonly string filename;
        private readonly ILogger logger = LogManager.Default.GetCurrentClassLogger();

        public ConfigurationManager()
        {
            if (!(typeof(T).GetCustomAttribute(typeof(ConfigurationAttribute)) is ConfigurationAttribute config))
            {
                throw new InvalidOperationException($"{typeof(T).Name} is not marked as config.");
            }

            if (string.IsNullOrWhiteSpace(config.Filename))
            {
                throw new InvalidOperationException("Invalid file name.");
            }

            this.filename = config.Filename;
            this.Config = new T();
        }

        public string DataFolder => "";

        public string Filename => Path.Combine(this.DataFolder, this.filename);

        public T Config { get; private set; }

        public void Load()
        {
            if (!File.Exists(this.Filename) || new FileInfo(this.Filename).Length == 0)
            {
                return;
            }

            try
            {
                using (var fileStream = File.OpenRead(this.Filename))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        var content = reader.ReadToEnd();
                        this.Config = JsonSerializer.Deserialize<T>(content);
                    }
                }
            }
            catch (Exception e)
            {
                this.logger.Debug(e, "Attempted to load config [{0}] failed.", this.Filename);
            }
        }

        public async Task LoadAsync()
        {
            if (!File.Exists(this.Filename) || new FileInfo(this.Filename).Length == 0)
            {
                this.Config = new T();
                return;
            }

            try
            {
                using (var fileStream = File.OpenRead(this.Filename))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        var content = await reader.ReadToEndAsync().ConfigureAwait(false);
                        this.Config = JsonSerializer.Deserialize<T>(content);
                    }
                }
            }
            catch (Exception e)
            {
                this.logger.Debug(e, "Attempted to load config [{0}] failed.", this.Filename);
                this.Config = new T();
            }
        }

        public void Save()
        {
            using (var fileStream = File.Create(this.Filename))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    var content = JsonSerializer.Serialize(this.Config);
                    writer.Write(content);
                }
            }
        }

        public async Task SaveAsync()
        {
            using (var fileStream = File.Create(this.Filename))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    var content = JsonSerializer.Serialize(this.Config);
                    await writer.WriteAsync(content).ConfigureAwait(false);
                }
            }
        }
    }
}