using System;
using System.Windows;
using System.Windows.Media;

namespace PhasmophobiaSaveEditor.Themes
{
    internal class FluentPalette : ThemePalette, IDynamicThemePalette
    {
        /// <summary>
        ///     Represents theme color variations.
        /// </summary>
        public enum ColorVariation
        {
            /// <summary>
            ///     Represents the Dark Fluent theme palette variation.
            /// </summary>
            Dark,

            /// <summary>
            ///     Represents the Light Fluent theme palette variation.
            /// </summary>
            Light
        }

        /// <summary>
        ///     Defines constants that specify the dimensions and appearance of ScrollViewer's ScrollBars.
        /// </summary>
        public enum ScrollViewerScrollBarsMode
        {
            /// <summary>
            ///     The ScrollBars appear as a narrow sliver and expand to normal size on MouseOver.
            /// </summary>
            Auto,

            /// <summary>
            ///     The ScrollBars appear always as a narrow sliver.
            /// </summary>
            Compact,

            /// <summary>
            ///     The ScrollBars appear always with their normal size.
            /// </summary>
            Normal
        }

        /// <summary>
        ///     Identifies the IsFreezable DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty IsFreezableProperty = DependencyProperty.RegisterAttached("IsFreezable", typeof(bool), typeof(FluentPalette), new PropertyMetadata(false, OnIsFreezablePropertyChanged));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.AccentColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AccentColorProperty = RegisterColor<FluentPalette>("AccentColor", 0xFF0099BC);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.AccentFocusedColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AccentFocusedColorProperty = RegisterColor<FluentPalette>("AccentFocusedColor", 0xFF15D7FF);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.AccentMouseOverColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AccentMouseOverColorProperty = RegisterColor<FluentPalette>("AccentMouseOverColor", 0xFF00BFE8);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.AccentPressedColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AccentPressedColorProperty = RegisterColor<FluentPalette>("AccentPressedColor", 0xFF0087A4);

        private static readonly DependencyProperty IsFrozenProperty = DependencyProperty.RegisterAttached("IsFrozen", typeof(bool), typeof(FluentPalette), new PropertyMetadata(false, OnIsFrozenChanged));
        private static FluentPalette frozenPalette;
        [ThreadStatic] private static FluentPalette palette;

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.BasicColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BasicColorProperty = RegisterMasterDynamicColor<FluentPalette>("BasicColor", 0x33000000, OnBasicDynamicColorChanged);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.AlternativeColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AlternativeColorProperty = RegisterColor<FluentPalette>("AlternativeColor", 0xFFF2F2F2);


        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.BasicSolidColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BasicSolidColorProperty = RegisterColor<FluentPalette>("BasicSolidColor", 0xFFCCCCCC);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ComplementaryColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ComplementaryColorProperty = RegisterColor<FluentPalette>("ComplementaryColor", 0xFFCCCCCC);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusBottom" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusBottomProperty = RegisterCornerRadius<FluentPalette>("CornerRadiusBottom", 0.0, 0.0, 9.0, 9.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusLeft" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusLeftProperty = RegisterCornerRadius<FluentPalette>("CornerRadiusLeft", 9.0, 0.0, 0.0, 9.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadius" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = RegisterDynamicCornerRadius<FluentPalette>("CornerRadius", 9.0, 9.0, 9.0, 9.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusRight" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusRightProperty = RegisterCornerRadius<FluentPalette>("CornerRadiusRight", 0.0, 9.0, 9.0, 0.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusTop" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusTopProperty = RegisterCornerRadius<FluentPalette>("CornerRadiusTop", 9.0, 9.0, 0.0, 0.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.DisabledOpacity" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisabledOpacityProperty = RegisterDouble<FluentPalette>("DisabledOpacity", 0.3);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FocusThickness" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FocusThicknessProperty = RegisterThickness<FluentPalette>("FocusThickness", 2.0, 2.0, 2.0, 2.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FontFamily" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FontFamilyProperty = RegisterFontFamily<FluentPalette>("FontFamily", "Segoe UI");

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FontSizeL" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FontSizeLProperty = RegisterDouble<FluentPalette>("FontSizeL", 13.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FontSize" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = RegisterDouble<FluentPalette>("FontSize", 12.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FontSizeS" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FontSizeSProperty = RegisterDouble<FluentPalette>("FontSizeS", 10.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FontSizeXL" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FontSizeXLProperty = RegisterDouble<FluentPalette>("FontSizeXL", 14.0);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.IconColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconColorProperty = RegisterColor<FluentPalette>("IconColor", 0xCC000000);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.InputOpacity" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputOpacityProperty = RegisterDouble<FluentPalette>("InputOpacity", 0.6);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.MainColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MainColorProperty = RegisterColor<FluentPalette>("MainColor", 0x19000000);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.MarkerColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MarkerColorProperty = RegisterColor<FluentPalette>("MarkerColor", 0xFF000000);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.MarkerInvertedColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MarkerInvertedColorProperty = RegisterColor<FluentPalette>("MarkerInvertedColor", 0xFFFFFFFF);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.MarkerMouseOverColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MarkerMouseOverColorProperty = RegisterColor<FluentPalette>("MarkerMouseOverColor", 0xFF000000);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.MouseOverColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MouseOverColorProperty = RegisterColor<FluentPalette>("MouseOverColor", 0x33000000);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.PressedColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PressedColorProperty = RegisterColor<FluentPalette>("PressedColor", 0x4C000000);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.PrimaryBackgroundColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryBackgroundColorProperty = RegisterColor<FluentPalette>("PrimaryBackgroundColor", 0xFFFFFFFF);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.PrimaryColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryColorProperty = RegisterColor<FluentPalette>("PrimaryColor", 0x66FFFFFF);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.PrimaryMouseOverColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PrimaryMouseOverColorProperty = RegisterColor<FluentPalette>("PrimaryMouseOverColor", 0xFFFFFFFF);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ReadOnlyBackgroundColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ReadOnlyBackgroundColorProperty = RegisterColor<FluentPalette>("ReadOnlyBackgroundColor", 0x00FFFFFF);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ReadOnlyBorderColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ReadOnlyBorderColorProperty = RegisterColor<FluentPalette>("ReadOnlyBorderColor", 0xFFCDCDCD);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ReadOnlyOpacity" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ReadOnlyOpacityProperty = RegisterDouble<FluentPalette>("ReadOnlyOpacity", 0.5);

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ScrollBarMode" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScrollBarModeProperty = DependencyProperty.Register("ScrollBarMode", typeof(ScrollViewerScrollBarsMode), typeof(FluentPalette), new PropertyMetadata(ScrollViewerScrollBarsMode.Normal, OnResourcePropertyChanged));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ValidationColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValidationColorProperty = RegisterColor<FluentPalette>("ValidationColor", 0xFFE81123);

        [ThreadStatic] private static ResourceDictionary resourceDictionary;

        /// <summary>
        ///     Prevents a default instance of the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" /> class from being
        ///     created.
        /// </summary>
        private FluentPalette() { }

        /// <summary>
        ///     Holds the Palette singleton instance.
        /// </summary>
        public static FluentPalette Palette
        {
            get
            {
                if (frozenPalette != null)
                {
                    return frozenPalette;
                }

                if (palette == null)
                {
                    palette = new FluentPalette();
                }

                return palette;
            }
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's accent elements.
        /// </summary>
        public Color AccentColor
        {
            get => (Color) this.GetValue(AccentColorProperty);
            set => this.SetValue(AccentColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's focus elements.
        /// </summary>
        public Color AccentFocusedColor
        {
            get => (Color) this.GetValue(AccentFocusedColorProperty);
            set => this.SetValue(AccentFocusedColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's mouse over accent elements.
        /// </summary>
        public Color AccentMouseOverColor
        {
            get => (Color) this.GetValue(AccentMouseOverColorProperty);
            set => this.SetValue(AccentMouseOverColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's pressed accent elements.
        /// </summary>
        public Color AccentPressedColor
        {
            get => (Color) this.GetValue(AccentPressedColorProperty);
            set => this.SetValue(AccentPressedColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's alternative elements.
        /// </summary>
        public Color AlternativeColor
        {
            get => (Color) this.GetValue(AlternativeColorProperty);
            set => this.SetValue(AlternativeColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's basic and border elements.
        /// </summary>
        public Color BasicColor
        {
            get => (Color) this.GetValue(BasicColorProperty);
            set => this.SetValue(BasicColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the solid color of the FluentTheme's basic and border elements.
        /// </summary>
        public Color BasicSolidColor
        {
            get => (Color) this.GetValue(BasicSolidColorProperty);
            set => this.SetValue(BasicSolidColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's complementary elements.
        /// </summary>
        public Color ComplementaryColor
        {
            get => (Color) this.GetValue(ComplementaryColorProperty);
            set => this.SetValue(ComplementaryColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadius" /> used in borders.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusBottom" /> used in borders.
        /// </summary>
        public CornerRadius CornerRadiusBottom
        {
            get => (CornerRadius) this.GetValue(CornerRadiusBottomProperty);
            set => this.SetValue(CornerRadiusBottomProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusLeft" /> used in borders.
        /// </summary>
        public CornerRadius CornerRadiusLeft
        {
            get => (CornerRadius) this.GetValue(CornerRadiusLeftProperty);
            set => this.SetValue(CornerRadiusLeftProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusRight" /> used in borders.
        /// </summary>
        public CornerRadius CornerRadiusRight
        {
            get => (CornerRadius) this.GetValue(CornerRadiusRightProperty);
            set => this.SetValue(CornerRadiusRightProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.CornerRadiusTop" /> used in borders.
        /// </summary>
        public CornerRadius CornerRadiusTop
        {
            get => (CornerRadius) this.GetValue(CornerRadiusTopProperty);
            set => this.SetValue(CornerRadiusTopProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.DisabledOpacity" /> used in disabled states.
        /// </summary>
        public double DisabledOpacity
        {
            get => (double) this.GetValue(DisabledOpacityProperty);
            set => this.SetValue(DisabledOpacityProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FocusThickness" /> used in focused states.
        /// </summary>
        public Thickness FocusThickness
        {
            get => (Thickness) this.GetValue(FocusThicknessProperty);
            set => this.SetValue(FocusThicknessProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.FontFamily" /> used in normal texts.
        /// </summary>
        public FontFamily FontFamily
        {
            get => (FontFamily) this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        ///     Gets or sets the font size for normal text.
        /// </summary>
        public double FontSize
        {
            get => (double) this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the font size for large text.
        /// </summary>
        public double FontSizeL
        {
            get => (double) this.GetValue(FontSizeLProperty);
            set => this.SetValue(FontSizeLProperty, value);
        }

        /// <summary>
        ///     Gets or sets the font size for small text.
        /// </summary>
        public double FontSizeS
        {
            get => (double) this.GetValue(FontSizeSProperty);
            set => this.SetValue(FontSizeSProperty, value);
        }

        /// <summary>
        ///     Gets or sets the font size for extra large text.
        /// </summary>
        public double FontSizeXL
        {
            get => (double) this.GetValue(FontSizeXLProperty);
            set => this.SetValue(FontSizeXLProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's icons and icon-like elements.
        /// </summary>
        public Color IconColor
        {
            get => (Color) this.GetValue(IconColorProperty);
            set => this.SetValue(IconColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.InputOpacity" /> used in the default states of
        ///     the input controls.
        /// </summary>
        public double InputOpacity
        {
            get => (double) this.GetValue(InputOpacityProperty);
            set => this.SetValue(InputOpacityProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's main elements.
        /// </summary>
        public Color MainColor
        {
            get => (Color) this.GetValue(MainColorProperty);
            set => this.SetValue(MainColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's marker and text elements.
        /// </summary>
        public Color MarkerColor
        {
            get => (Color) this.GetValue(MarkerColorProperty);
            set => this.SetValue(MarkerColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's inverted marker elements.
        /// </summary>
        public Color MarkerInvertedColor
        {
            get => (Color) this.GetValue(MarkerInvertedColorProperty);
            set => this.SetValue(MarkerInvertedColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's marker and text hovered elements.
        /// </summary>
        public Color MarkerMouseOverColor
        {
            get => (Color) this.GetValue(MarkerMouseOverColorProperty);
            set => this.SetValue(MarkerMouseOverColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's main hovered elements.
        /// </summary>
        public Color MouseOverColor
        {
            get => (Color) this.GetValue(MouseOverColorProperty);
            set => this.SetValue(MouseOverColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's main pressed elements.
        /// </summary>
        public Color PressedColor
        {
            get => (Color) this.GetValue(PressedColorProperty);
            set => this.SetValue(PressedColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's primary background elements.
        /// </summary>
        public Color PrimaryBackgroundColor
        {
            get => (Color) this.GetValue(PrimaryBackgroundColorProperty);
            set => this.SetValue(PrimaryBackgroundColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's primary elements.
        /// </summary>
        public Color PrimaryColor
        {
            get => (Color) this.GetValue(PrimaryColorProperty);
            set => this.SetValue(PrimaryColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's mouse over primary elements.
        /// </summary>
        public Color PrimaryMouseOverColor
        {
            get => (Color) this.GetValue(PrimaryMouseOverColorProperty);
            set => this.SetValue(PrimaryMouseOverColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's read only background elements.
        /// </summary>
        public Color ReadOnlyBackgroundColor
        {
            get => (Color) this.GetValue(ReadOnlyBackgroundColorProperty);
            set => this.SetValue(ReadOnlyBackgroundColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's read only border elements.
        /// </summary>
        public Color ReadOnlyBorderColor
        {
            get => (Color) this.GetValue(ReadOnlyBorderColorProperty);
            set => this.SetValue(ReadOnlyBorderColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ReadOnlyOpacity" /> used in readonly states.
        /// </summary>
        public double ReadOnlyOpacity
        {
            get => (double) this.GetValue(ReadOnlyOpacityProperty);
            set => this.SetValue(ReadOnlyOpacityProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.FluentPalette.ScrollBarMode" /> used for the ScrollViewer's
        ///     ScrollBars default appearance within the Fluent theme.
        ///     The default value is Auto - the ScrollBars appear as a narrow sliver and expand to their normal size on MouseOver.
        /// </summary>
        public ScrollViewerScrollBarsMode ScrollBarMode
        {
            get => (ScrollViewerScrollBarsMode) this.GetValue(ScrollBarModeProperty);
            set => this.SetValue(ScrollBarModeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the FluentTheme's validation elements.
        /// </summary>
        public Color ValidationColor
        {
            get => (Color) this.GetValue(ValidationColorProperty);
            set => this.SetValue(ValidationColorProperty, value);
        }

        internal static ResourceDictionary ResourceDictionary
        {
            get
            {
                if (resourceDictionary == null)
                {
                    resourceDictionary = new ResourceDictionary();
                    InitializeThemeDictionary();
                }

                return resourceDictionary;
            }
        }

        internal override void FreezeOverride()
        {
            frozenPalette = this;
            this.SetValue(IsFrozenProperty, true);
        }

        internal override void OnResourcePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ScrollBarModeProperty)
            {
                SetResource(ResourceDictionary, FluentResourceKey.ScrollBarMode, Palette.ScrollBarMode);
            }
            else if (e.Property == AccentColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentBrush, new SolidColorBrush(Palette.AccentColor));
            }

            if (e.Property == AccentMouseOverColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentMouseOverBrush, new SolidColorBrush(Palette.AccentMouseOverColor));
                return;
            }

            if (e.Property == AccentPressedColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentPressedBrush, new SolidColorBrush(Palette.AccentPressedColor));
                return;
            }

            if (e.Property == AccentFocusedColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentFocusedBrush, new SolidColorBrush(Palette.AccentFocusedColor));
                return;
            }

            if (e.Property == BasicColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.BasicBrush, new SolidColorBrush(Palette.BasicColor));
                return;
            }

            if (e.Property == BasicSolidColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.BasicSolidBrush, new SolidColorBrush(Palette.BasicSolidColor));
                return;
            }

            if (e.Property == IconColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.IconBrush, new SolidColorBrush(Palette.IconColor));
                return;
            }

            if (e.Property == MainColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MainBrush, new SolidColorBrush(Palette.MainColor));
                return;
            }

            if (e.Property == MarkerColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MarkerBrush, new SolidColorBrush(Palette.MarkerColor));
                return;
            }

            if (e.Property == MarkerMouseOverColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MarkerMouseOverBrush, new SolidColorBrush(Palette.MarkerMouseOverColor));
                return;
            }

            if (e.Property == ValidationColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ValidationBrush, new SolidColorBrush(Palette.ValidationColor));
                return;
            }

            if (e.Property == ComplementaryColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ComplementaryBrush, new SolidColorBrush(Palette.ComplementaryColor));
                return;
            }

            if (e.Property == MouseOverColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MouseOverBrush, new SolidColorBrush(Palette.MouseOverColor));
                return;
            }

            if (e.Property == PressedColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PressedBrush, new SolidColorBrush(Palette.PressedColor));
                return;
            }

            if (e.Property == AlternativeColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AlternativeBrush, new SolidColorBrush(Palette.AlternativeColor));
                return;
            }

            if (e.Property == MarkerInvertedColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MarkerInvertedBrush, new SolidColorBrush(Palette.MarkerInvertedColor));
                return;
            }

            if (e.Property == PrimaryColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PrimaryBrush, new SolidColorBrush(Palette.PrimaryColor));
                return;
            }

            if (e.Property == PrimaryBackgroundColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PrimaryBackgroundBrush, new SolidColorBrush(Palette.PrimaryBackgroundColor));
                return;
            }

            if (e.Property == PrimaryMouseOverColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PrimaryMouseOverBrush, new SolidColorBrush(Palette.PrimaryMouseOverColor));
                return;
            }

            if (e.Property == ReadOnlyBackgroundColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ReadOnlyBackgroundBrush, new SolidColorBrush(Palette.ReadOnlyBackgroundColor));
                return;
            }

            if (e.Property == ReadOnlyBorderColorProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ReadOnlyBorderBrush, new SolidColorBrush(Palette.ReadOnlyBorderColor));
                return;
            }

            if (e.Property == FontSizeSProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSizeS, Palette.FontSizeS);
                return;
            }

            if (e.Property == FontSizeProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSize, Palette.FontSize);
                return;
            }

            if (e.Property == FontSizeLProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSizeL, Palette.FontSizeL);
                return;
            }

            if (e.Property == FontSizeXLProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSizeXL, Palette.FontSizeXL);
                return;
            }

            if (e.Property == FontFamilyProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontFamily, Palette.FontFamily);
                return;
            }

            if (e.Property == CornerRadiusProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadius, Palette.CornerRadius);
                return;
            }

            if (e.Property == CornerRadiusTopProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusTop, Palette.CornerRadiusTop);
                return;
            }

            if (e.Property == CornerRadiusBottomProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusBottom, Palette.CornerRadiusBottom);
                return;
            }

            if (e.Property == CornerRadiusLeftProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusLeft, Palette.CornerRadiusLeft);
                return;
            }

            if (e.Property == CornerRadiusRightProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusRight, Palette.CornerRadiusRight);
                return;
            }

            if (e.Property == DisabledOpacityProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.DisabledOpacity, Palette.DisabledOpacity);
                return;
            }

            if (e.Property == InputOpacityProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.InputOpacity, Palette.InputOpacity);
                return;
            }

            if (e.Property == ReadOnlyOpacityProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ReadOnlyOpacity, Palette.ReadOnlyOpacity);
                return;
            }

            if (e.Property == FocusThicknessProperty)
            {
                FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FocusThickness, Palette.FocusThickness);
            }
        }

        /// <summary>
        ///     Gets the IsFreezable value from a DependencyObject.
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject.</param>
        /// <returns>The IsFreezable value.</returns>
        /// +
        public static bool GetIsFreezable(DependencyObject dependencyObject) => (bool) dependencyObject.GetValue(IsFreezableProperty);

        /// <summary>
        ///     Loads a preset.
        /// </summary>
        /// <param name="preset">The color preset.</param>
        public static void LoadPreset(ColorVariation preset)
        {
            if (preset == ColorVariation.Dark)
            {
                Palette.MainColor = ColorFromLongValue(0x33FFFFFF);
                Palette.MarkerColor = ColorFromLongValue(0xFFFFFFFF);
                Palette.MarkerInvertedColor = ColorFromLongValue(0xFFFFFFFF);
                Palette.MouseOverColor = ColorFromLongValue(0x4CFFFFFF);
                Palette.PressedColor = ColorFromLongValue(0x26FFFFFF);
                Palette.PrimaryColor = ColorFromLongValue(0x66FFFFFF);
                Palette.PrimaryBackgroundColor = ColorFromLongValue(0xFF000000);
                Palette.PrimaryMouseOverColor = ColorFromLongValue(0xFFFFFFFF);
                Palette.AlternativeColor = ColorFromLongValue(0xFF2B2B2B);
                Palette.ComplementaryColor = ColorFromLongValue(0xFF333333);
                Palette.IconColor = ColorFromLongValue(0xCCFFFFFF);
                Palette.ReadOnlyBorderColor = ColorFromLongValue(0xFF4C4C4C);
                Palette.BasicColor = ColorFromLongValue(0x4CFFFFFF);
                return;
            }

            Palette.MainColor = (Color) MainColorProperty.DefaultMetadata.DefaultValue;
            Palette.MarkerColor = (Color) MarkerColorProperty.DefaultMetadata.DefaultValue;
            Palette.MarkerInvertedColor = (Color) MarkerInvertedColorProperty.DefaultMetadata.DefaultValue;
            Palette.MouseOverColor = (Color) MouseOverColorProperty.DefaultMetadata.DefaultValue;
            Palette.PressedColor = (Color) PressedColorProperty.DefaultMetadata.DefaultValue;
            Palette.PrimaryColor = (Color) PrimaryColorProperty.DefaultMetadata.DefaultValue;
            Palette.PrimaryBackgroundColor = (Color) PrimaryBackgroundColorProperty.DefaultMetadata.DefaultValue;
            Palette.PrimaryMouseOverColor = (Color) PrimaryMouseOverColorProperty.DefaultMetadata.DefaultValue;
            Palette.AlternativeColor = (Color) AlternativeColorProperty.DefaultMetadata.DefaultValue;
            Palette.ComplementaryColor = (Color) ComplementaryColorProperty.DefaultMetadata.DefaultValue;
            Palette.IconColor = (Color) IconColorProperty.DefaultMetadata.DefaultValue;
            Palette.ReadOnlyBorderColor = (Color) ReadOnlyBorderColorProperty.DefaultMetadata.DefaultValue;
            Palette.BasicColor = (Color) BasicColorProperty.DefaultMetadata.DefaultValue;
        }

        /// <summary>
        ///     Sets the IsFreezable value from a DependencyObject.
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject.</param>
        /// <param name="value">The IsFreezable value.</param>
        public static void SetIsFreezable(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsFreezableProperty, value);
        }

        private static void InitializeThemeDictionary()
        {
            SetResource(ResourceDictionary, FluentResourceKey.ScrollBarMode, ScrollViewerScrollBarsMode.Auto);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentBrush, new SolidColorBrush(Palette.AccentColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentMouseOverBrush, new SolidColorBrush(Palette.AccentMouseOverColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentPressedBrush, new SolidColorBrush(Palette.AccentPressedColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AccentFocusedBrush, new SolidColorBrush(Palette.AccentFocusedColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.BasicBrush, new SolidColorBrush(Palette.BasicColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.BasicSolidBrush, new SolidColorBrush(Palette.BasicSolidColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.IconBrush, new SolidColorBrush(Palette.IconColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MainBrush, new SolidColorBrush(Palette.MainColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MarkerBrush, new SolidColorBrush(Palette.MarkerColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ValidationBrush, new SolidColorBrush(Palette.ValidationColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MouseOverBrush, new SolidColorBrush(Palette.MouseOverColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PressedBrush, new SolidColorBrush(Palette.PressedColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ComplementaryBrush, new SolidColorBrush(Palette.ComplementaryColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.AlternativeBrush, new SolidColorBrush(Palette.AlternativeColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MarkerMouseOverBrush, new SolidColorBrush(Palette.MarkerMouseOverColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.MarkerInvertedBrush, new SolidColorBrush(Palette.MarkerInvertedColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PrimaryBrush, new SolidColorBrush(Palette.PrimaryColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PrimaryBackgroundBrush, new SolidColorBrush(Palette.PrimaryBackgroundColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.PrimaryMouseOverBrush, new SolidColorBrush(Palette.PrimaryMouseOverColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ReadOnlyBackgroundBrush, new SolidColorBrush(Palette.ReadOnlyBackgroundColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ReadOnlyBorderBrush, new SolidColorBrush(Palette.ReadOnlyBorderColor));
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSizeS, Palette.FontSizeS);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSize, Palette.FontSize);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSizeL, Palette.FontSizeL);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontSizeXL, Palette.FontSizeXL);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FontFamily, Palette.FontFamily);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadius, Palette.CornerRadius);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusTop, Palette.CornerRadiusTop);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusBottom, Palette.CornerRadiusBottom);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusLeft, Palette.CornerRadiusLeft);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.CornerRadiusRight, Palette.CornerRadiusRight);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.DisabledOpacity, Palette.DisabledOpacity);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.InputOpacity, Palette.InputOpacity);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.ReadOnlyOpacity, Palette.ReadOnlyOpacity);
            FreezeAndSetResource(ResourceDictionary, FluentResourceKey.FocusThickness, Palette.FocusThickness);
        }

        private static void OnBasicDynamicColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FluentPalette fluentPalette)
            {
                var primaryBackgroundColor = fluentPalette.PrimaryBackgroundColor;
                var targetColor = (Color) e.NewValue;
                fluentPalette.BasicSolidColor = RgbFromArgbAndBackgroundColor(targetColor, primaryBackgroundColor);
            }

            if (d is ThemePalette themePalette)
            {
                themePalette.OnResourcePropertyChanged(e);
            }
        }

        private static void OnIsFreezablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnIsFreezableChanged(d, e, IsFrozenProperty, Palette);
        }

        private static void OnIsFrozenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OnIsFrozenChanged(d, e);
        }
    }
}