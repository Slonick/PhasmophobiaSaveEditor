using System;

namespace PhasmophobiaSaveEditor.Configuration.Attributes
{
    public class ConfigurationAttribute : Attribute
    {
        public string Filename { get; set; }
    }
}