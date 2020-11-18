using System;

namespace PhasmophobiaSaveEditor.Logging.Layouts
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class LogLayoutAttribute : Attribute
    {
        public LogLayoutAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}