using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace PhasmophobiaSaveEditor.Utils
{
    public static class JsonSerializer
    {
        public static T Deserialize<T>(string json, bool useSimpleDictionaryFormat = true)
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                try
                {
                    var settings = new DataContractJsonSerializerSettings {UseSimpleDictionaryFormat = useSimpleDictionaryFormat};
                    var serializer = new DataContractJsonSerializer(typeof(T), settings);
                    return (T) serializer.ReadObject(memoryStream);
                }
                finally
                {
                    Thread.CurrentThread.CurrentCulture = currentCulture;
                }
            }
        }

        public static string Serialize<T>(T obj, bool useSimpleDictionaryFormat = true)
        {
            using (var memoryStream = new MemoryStream())
            {
                var settings = new DataContractJsonSerializerSettings {UseSimpleDictionaryFormat = useSimpleDictionaryFormat};
                var serializer = new DataContractJsonSerializer(typeof(T), settings);

                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(memoryStream, Encoding.UTF8, true))
                {
                    serializer.WriteObject(writer, obj);
                }

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}