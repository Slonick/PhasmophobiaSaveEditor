using System.Windows;
using System.Windows.Media;
using PhasmophobiaSaveEditor.Themes;

namespace PhasmophobiaSaveEditor.Controls.Fluent
{
    /// <summary>
    ///     Defines a set of attached properties that affect the visual appearance and coloring of elements in the Fluent
    ///     theme.
    /// </summary>
    public class FluentAssist
    {
        /// <summary>
        ///     Identifies the <see cref="F:PhasmophobiaSaveEditor.Controls.Controls.Fluent.FluentAssist.CheckedBrushProperty"/> for the
        ///     element. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty CheckedBrushProperty = DependencyProperty.RegisterAttached("CheckedBrush", typeof(Brush), typeof(FluentAssist), new FrameworkPropertyMetadata(null));

        /// <summary>
        ///     Identifies attached <see cref="T:PhasmophobiaSaveEditor.Controls.Controls.Fluent.FluentAssist.CornerRadius"/> for controls
        ///     which do not own one. This is an
        ///     attached property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(FluentAssist), new FrameworkPropertyMetadata(default(CornerRadius)));

        /// <summary>
        ///     Identifies the <see cref="F:PhasmophobiaSaveEditor.Controls.Controls.Fluent.FluentAssist.FocusBrushProperty"/> for the
        ///     element. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty FocusBrushProperty = DependencyProperty.RegisterAttached("FocusBrush", typeof(Brush), typeof(FluentAssist), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(FluentAssist), new PropertyMetadata(true));

        /// <summary>
        ///     Identifies the <see cref="F:PhasmophobiaSaveEditor.Controls.Controls.Fluent.FluentAssist.MouseOverBrushProperty"/> for
        ///     the element. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty MouseOverBrushProperty = DependencyProperty.RegisterAttached("MouseOverBrush", typeof(Brush), typeof(FluentAssist), new FrameworkPropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="F:PhasmophobiaSaveEditor.Controls.Controls.Fluent.FluentAssist.PressedBrushProperty"/> for the
        ///     element. This is an attached property.
        /// </summary>
        public static readonly DependencyProperty PressedBrushProperty = DependencyProperty.RegisterAttached("PressedBrush", typeof(Brush), typeof(FluentAssist), new FrameworkPropertyMetadata(null));

        /// <summary>
        ///     Identifies the ScrollBarModeResourceKey attached property.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         This property is used for internal purposes only.
        ///     </para>
        /// </remarks>
        public static readonly DependencyProperty ScrollBarModeResourceKeyProperty =
            DependencyProperty.RegisterAttached("ScrollBarModeResourceKey", typeof(FluentPalette.ScrollViewerScrollBarsMode), typeof(FluentAssist), new PropertyMetadata(FluentPalette.ScrollViewerScrollBarsMode.Normal));

        /// <summary>
        ///     Gets the Brush for the checked/selected state of the specified element.
        /// </summary>
        public static Brush GetCheckedBrush(DependencyObject element) => (Brush) element.GetValue(CheckedBrushProperty);

        /// <summary>
        ///     Gets the <see cref="T:System.Windows.CornerRadius"/> for the specified element.
        /// </summary>
        public static CornerRadius GetCornerRadius(DependencyObject element) => (CornerRadius) element.GetValue(CornerRadiusProperty);

        /// <summary>
        ///     Gets the Brush for the focused state of the specified element.
        /// </summary>
        public static Brush GetFocusBrush(DependencyObject element) => (Brush) element.GetValue(FocusBrushProperty);

        public static bool GetIsEnabled(DependencyObject element) => (bool) element.GetValue(IsEnabledProperty);

        /// <summary>
        ///     Gets the Brush for the mouse over state of the specified element.
        /// </summary>
        public static Brush GetMouseOverBrush(DependencyObject element) => (Brush) element.GetValue(MouseOverBrushProperty);

        /// <summary>
        ///     Gets the Brush for the pressed state of the specified element.
        /// </summary>
        public static Brush GetPressedBrush(DependencyObject element) => (Brush) element.GetValue(PressedBrushProperty);

        /// <summary>
        ///     Gets the scrollbar mode resource key.
        /// </summary>
        public static string GetScrollBarModeResourceKey(DependencyObject d)
        {
            return (string) d.GetValue(ScrollBarModeResourceKeyProperty);
        }

        /// <summary>
        ///     Sets a Brush for the checked/selected state of the specified element.
        /// </summary>
        public static void SetCheckedBrush(DependencyObject element, Brush value) => element.SetValue(CheckedBrushProperty, value);

        /// <summary>
        ///     Sets the <see cref="T:System.Windows.CornerRadius"/> for the specified element.
        /// </summary>
        public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);

        /// <summary>
        ///     Sets a Brush for the focused state of the specified element.
        /// </summary>
        public static void SetFocusBrush(DependencyObject element, Brush value) => element.SetValue(FocusBrushProperty, value);

        public static void SetIsEnabled(DependencyObject element, bool value) => element.SetValue(IsEnabledProperty, value);

        /// <summary>
        ///     Sets a Brush for the mouse over state of the specified element.
        /// </summary>
        public static void SetMouseOverBrush(DependencyObject element, Brush value) => element.SetValue(MouseOverBrushProperty, value);

        /// <summary>
        ///     Sets a Brush for the pressed state of the specified element.
        /// </summary>
        public static void SetPressedBrush(DependencyObject element, Brush value) => element.SetValue(PressedBrushProperty, value);

        /// <summary>
        ///     Sets the scrollbar mode resource key.
        /// </summary>
        public static void SetScrollBarModeResourceKey(DependencyObject d, string value)
        {
            d.SetValue(ScrollBarModeResourceKeyProperty, value);
        }
    }
}