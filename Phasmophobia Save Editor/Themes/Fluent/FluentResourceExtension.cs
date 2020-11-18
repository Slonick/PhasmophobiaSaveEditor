using System.ComponentModel;

namespace PhasmophobiaSaveEditor.Themes
{
    /// <summary>
	/// A <see cref="T:System.Windows.Markup.MarkupExtension" /> that allows access to the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" /> resources from XAML.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	public sealed class FluentResourceExtension : ThemeResourceExtension<FluentResourceKey>
	{
        public FluentResourceExtension(FluentResourceKey resourceKey)
        {
            this.ResourceKey = resourceKey;
        }
	}
}
