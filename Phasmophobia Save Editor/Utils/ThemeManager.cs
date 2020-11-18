using System;
using PhasmophobiaSaveEditor.Infrastructures;
using PhasmophobiaSaveEditor.Themes;

namespace PhasmophobiaSaveEditor.Utils
{
    public static class ThemeManager
    {
        public static void ChangeTheme(Theme theme)
        {
            switch (theme)
            {
                case Theme.Light:
                    FluentPalette.LoadPreset(FluentPalette.ColorVariation.Light);
                    break;
                case Theme.Dark:
                    FluentPalette.LoadPreset(FluentPalette.ColorVariation.Dark);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(theme), theme, null);
            }
        }
    }
}