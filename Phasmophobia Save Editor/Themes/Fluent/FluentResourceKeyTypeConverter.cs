using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PhasmophobiaSaveEditor.Themes
{
    /// <summary>
    ///     Used to convert <see cref="T:System.Windows.ResourceKey" /> types used in
    ///     <see cref="T:PhasmophobiaSaveEditor.FluentResourceKey" />.
    /// </summary>
    public class FluentResourceKeyTypeConverter : ThemeResourceKeyTypeConverter<FluentResourceKey>
    {
        private static readonly Dictionary<string, ResourceKey> keys;

        private static readonly Lazy<StandardValuesCollection> standardValues = new Lazy<StandardValuesCollection>(() => new StandardValuesCollection(keys.Keys.ToList()));

        static FluentResourceKeyTypeConverter()
        {
            keys = new Dictionary<string, ResourceKey>
            {
                {FluentResourceKeyID.ScrollBarMode.ToString(), FluentResourceKey.ScrollBarMode},
                {FluentResourceKeyID.AccentBrush.ToString(), FluentResourceKey.AccentBrush},
                {FluentResourceKeyID.AccentMouseOverBrush.ToString(), FluentResourceKey.AccentMouseOverBrush},
                {FluentResourceKeyID.AccentPressedBrush.ToString(), FluentResourceKey.AccentPressedBrush},
                {FluentResourceKeyID.AccentFocusedBrush.ToString(), FluentResourceKey.AccentFocusedBrush},
                {FluentResourceKeyID.MouseOverBrush.ToString(), FluentResourceKey.MouseOverBrush},
                {FluentResourceKeyID.PressedBrush.ToString(), FluentResourceKey.PressedBrush},
                {FluentResourceKeyID.BasicBrush.ToString(), FluentResourceKey.BasicBrush},
                {FluentResourceKeyID.BasicSolidBrush.ToString(), FluentResourceKey.BasicSolidBrush},
                {FluentResourceKeyID.IconBrush.ToString(), FluentResourceKey.IconBrush},
                {FluentResourceKeyID.MainBrush.ToString(), FluentResourceKey.MainBrush},
                {FluentResourceKeyID.MarkerBrush.ToString(), FluentResourceKey.MarkerBrush},
                {FluentResourceKeyID.MarkerMouseOverBrush.ToString(), FluentResourceKey.MarkerMouseOverBrush},
                {FluentResourceKeyID.ValidationBrush.ToString(), FluentResourceKey.ValidationBrush},
                {FluentResourceKeyID.ComplementaryBrush.ToString(), FluentResourceKey.ComplementaryBrush},
                {FluentResourceKeyID.AlternativeBrush.ToString(), FluentResourceKey.AlternativeBrush},
                {FluentResourceKeyID.MarkerInvertedBrush.ToString(), FluentResourceKey.MarkerInvertedBrush},
                {FluentResourceKeyID.PrimaryBrush.ToString(), FluentResourceKey.PrimaryBrush},
                {FluentResourceKeyID.PrimaryBackgroundBrush.ToString(), FluentResourceKey.PrimaryBackgroundBrush},
                {FluentResourceKeyID.PrimaryMouseOverBrush.ToString(), FluentResourceKey.PrimaryMouseOverBrush},
                {FluentResourceKeyID.ReadOnlyBackgroundBrush.ToString(), FluentResourceKey.ReadOnlyBackgroundBrush},
                {FluentResourceKeyID.ReadOnlyBorderBrush.ToString(), FluentResourceKey.ReadOnlyBorderBrush},
                {FluentResourceKeyID.FontSizeS.ToString(), FluentResourceKey.FontSizeS},
                {FluentResourceKeyID.FontSize.ToString(), FluentResourceKey.FontSize},
                {FluentResourceKeyID.FontSizeL.ToString(), FluentResourceKey.FontSizeL},
                {FluentResourceKeyID.FontSizeXL.ToString(), FluentResourceKey.FontSizeXL},
                {FluentResourceKeyID.FontFamily.ToString(), FluentResourceKey.FontFamily},
                {FluentResourceKeyID.CornerRadius.ToString(), FluentResourceKey.CornerRadius},
                {FluentResourceKeyID.CornerRadiusTop.ToString(), FluentResourceKey.CornerRadiusTop},
                {FluentResourceKeyID.CornerRadiusBottom.ToString(), FluentResourceKey.CornerRadiusBottom},
                {FluentResourceKeyID.CornerRadiusLeft.ToString(), FluentResourceKey.CornerRadiusLeft},
                {FluentResourceKeyID.CornerRadiusRight.ToString(), FluentResourceKey.CornerRadiusRight},
                {FluentResourceKeyID.DisabledOpacity.ToString(), FluentResourceKey.DisabledOpacity},
                {FluentResourceKeyID.InputOpacity.ToString(), FluentResourceKey.InputOpacity},
                {FluentResourceKeyID.ReadOnlyOpacity.ToString(), FluentResourceKey.ReadOnlyOpacity},
                {FluentResourceKeyID.FocusThickness.ToString(), FluentResourceKey.FocusThickness}
            };
        }

        internal override IDictionary<string, ResourceKey> Keys
        {
            get => keys;
        }

        /// <inheritdoc />
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) => standardValues.Value;

        /// <inheritdoc />
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
    }
}