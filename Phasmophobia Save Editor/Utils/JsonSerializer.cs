using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace PhasmophobiaSaveEditor.Utils
{
    public static class JsonSerializer
    {
        private const string IndentString = "    ";

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

        public static string FormatJson(string str)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ToList().ForEach(item => sb.Append(IndentString));
                        }

                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ToList().ForEach(item => sb.Append(IndentString));
                        }

                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        var escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ToList().ForEach(item => sb.Append(IndentString));
                        }

                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }

            return sb.ToString();
        }
    }
}