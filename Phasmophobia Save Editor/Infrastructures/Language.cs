using PhasmophobiaSaveEditor.Attributes;

namespace PhasmophobiaSaveEditor.Infrastructures
{
    public enum Language
    {
        [CultureName(Suffix = "en-US")] English = 0,

        [CultureName(Suffix = "ru-RU")] Russian = 1
    }
}