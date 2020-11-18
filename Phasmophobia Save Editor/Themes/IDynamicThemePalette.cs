using System.Windows;

namespace PhasmophobiaSaveEditor.Themes
{
    internal interface IDynamicThemePalette
    {
        CornerRadius CornerRadiusBottom { get; set; }

        CornerRadius CornerRadiusLeft { get; set; }

        CornerRadius CornerRadiusRight { get; set; }

        CornerRadius CornerRadiusTop { get; set; }
    }
}