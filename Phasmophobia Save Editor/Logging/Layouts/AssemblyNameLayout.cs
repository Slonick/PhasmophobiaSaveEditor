using System.Reflection;
using System.Text;

namespace PhasmophobiaSaveEditor.Logging.Layouts
{
    [LogLayout("assembly-name")]
    internal sealed class AssemblyNameLayout : ILogLayout
    {
        private string assemblyName;

        public void Append(StringBuilder builder, LogEventInfo logEvent, string layoutFormat)
        {
            string result;
            if ((result = this.assemblyName) == null)
            {
                result = this.assemblyName = this.GetName();
            }

            builder.Append(result);
        }

        private Assembly GetAssembly() => Assembly.GetEntryAssembly();

        private string GetName()
        {
            var assembly = this.GetAssembly();
            return this.GetName(assembly);
        }

        private string GetName(Assembly assembly)
        {
            if (assembly == null)
            {
                return null;
            }

            var assemblyTitle = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            return assemblyTitle?.Title;
        }
    }
}