using System.ComponentModel;
using System.Windows;

namespace PhasmophobiaSaveEditor.Themes
{
    /// <summary>
    ///     A <see cref="T:System.Windows.Markup.MarkupExtension" /> that allows access to the
    ///     <see cref="T:PhasmophobiaSaveEditor.ThemePalette" /> resources from XAML.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class ThemeResourceExtension<T> : DynamicResourceExtension where T : ResourceKey
    {
        internal ThemeResourceExtension() { }

        /// <summary>
        ///     Gets or sets the <see cref="T:PhasmophobiaSaveEditor.Windows8ResourceKey" /> for which a resource would be
        ///     retrieved from the <see cref="T:PhasmophobiaSaveEditor.Windows8Palette" />.
        /// </summary>
        public new T ResourceKey
        {
            get => base.ResourceKey as T;
            set => base.ResourceKey = value;
        }
    }
}