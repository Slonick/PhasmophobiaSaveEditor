using System.Windows;

namespace PhasmophobiaSaveEditor.Themes
{
    /// <summary>
    ///     A <see cref="T:System.Windows.ResourceDictionary" /> that merges the resources from the
    ///     <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />.
    /// </summary>
    public sealed class FluentResourceDictionary : ResourceDictionary
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PhasmophobiaSaveEditor.FluentResourceDictionary" /> class.
        /// </summary>
        public FluentResourceDictionary()
        {
            this.MergedDictionaries.Add(FluentPalette.ResourceDictionary);
        }
    }
}