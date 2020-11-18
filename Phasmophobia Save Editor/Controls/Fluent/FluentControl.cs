using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace PhasmophobiaSaveEditor.Controls.Fluent
{
    /// <summary>
    ///     Fluent control to handle the visual effects in the Fluent theme.
    /// </summary>
    [ContentProperty("Content")]
    public class FluentControl : Control
    {
        private const string TemplateStateMouseIn = "MouseIn";
        private const string TemplateStateMouseOut = "MouseOut";
        private const string TemplateStateMousePressed = "MousePressed";
        private const string TemplateStateNormal = "Normal";

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.Content" /> dependency
        ///     property.
        ///     Default Value: null.
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(FluentControl), new PropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.RippleBrush" /> dependency
        ///     property.
        /// </summary>
        public static readonly DependencyProperty RippleBrushProperty = DependencyProperty.Register("RippleBrush", typeof(Brush), typeof(FluentControl), new PropertyMetadata(null));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.BorderGradient" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderGradientProperty =
            DependencyProperty.Register("BorderGradient", typeof(Brush), typeof(FluentControl), new PropertyMetadata(null, null, OnBorderGradientPropertyCoerce));


        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.EffectMode" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty EffectModeProperty =
            DependencyProperty.Register("EffectMode", typeof(FluentControlEffectMode), typeof(FluentControl), new PropertyMetadata(FluentControlEffectMode.Ripple));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.IsPressed" /> dependency
        ///     property.
        /// </summary>
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(FluentControl), new PropertyMetadata(false));

        /// <summary>
        ///     Identifies the key for the
        ///     <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.IsRippleEnabled" /> dependency property.
        ///     Default value: true.
        /// </summary>
        public static readonly DependencyProperty IsRippleEnabledProperty =
            DependencyProperty.Register("IsRippleEnabled", typeof(bool), typeof(FluentControl), new PropertyMetadata(true, OnIsRippleEnabledChanged));

        private static readonly DependencyPropertyKey RippleSizePropertyKey =
            DependencyProperty.RegisterReadOnly("RippleSize", typeof(double), typeof(FluentControl), new PropertyMetadata(0.0));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.RippleSize" /> dependency
        ///     property.
        /// </summary>
        public static readonly DependencyProperty RippleSizeProperty = RippleSizePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey RippleXPropertyKey =
            DependencyProperty.RegisterReadOnly("RippleX", typeof(double), typeof(FluentControl), new PropertyMetadata(0.0));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.RippleX" /> dependency
        ///     property.
        /// </summary>
        public static readonly DependencyProperty RippleXProperty =
            RippleXPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey RippleYPropertyKey =
            DependencyProperty.RegisterReadOnly("RippleY", typeof(double), typeof(FluentControl), new PropertyMetadata(0.0));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.RippleY" /> dependency
        ///     property.
        /// </summary>
        public static readonly DependencyProperty RippleYProperty = RippleYPropertyKey.DependencyProperty;

        internal static readonly HashSet<FluentControl> PressedInstances = new HashSet<FluentControl>();

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.IsSmartClipped" /> dependency
        ///     property.
        ///     Default Value: false.
        /// </summary>
        public static readonly DependencyProperty IsSmartClippedProperty =
            DependencyProperty.Register("IsSmartClipped", typeof(bool), typeof(FluentControl), new PropertyMetadata(false, OnIsSmartClippedChanged));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.IsHighlighted" /> dependency
        ///     property.
        ///     Default value: false.
        /// </summary>
        [Browsable(false)] [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(FluentControl), new PropertyMetadata(false));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.CornerRadius" /> dependency
        ///     property.
        ///     Default Value: CornerRadius(0).
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FluentControl), new PropertyMetadata(new CornerRadius(0.0), OnCornerRadiusChanged));

        static FluentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentControl), new FrameworkPropertyMetadata(typeof(FluentControl)));

            EventManager.RegisterClassHandler(typeof(Control), Mouse.PreviewMouseUpEvent, new MouseButtonEventHandler(PreviewMouseButtonUpEventHandler), true);
            EventManager.RegisterClassHandler(typeof(ButtonBase), Mouse.MouseMoveEvent, new MouseEventHandler(ButtonBaseMouseMoveEventHandler), true);
            EventManager.RegisterClassHandler(typeof(Popup), Mouse.PreviewMouseUpEvent, new MouseButtonEventHandler(PreviewMouseButtonUpEventHandler), true);
        }

        /// <summary>
        ///     Gets or sets the border gradient brush. The value will be cloned to prevent invalid operations on frozen resources.
        /// </summary>
        public Brush BorderGradient
        {
            get => (Brush) this.GetValue(BorderGradientProperty);
            set => this.SetValue(BorderGradientProperty, value);
        }

        /// <summary>
        ///     Gets or sets the Content of the <see cref="T:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl" />.
        /// </summary>
        public object Content
        {
            get => this.GetValue(ContentProperty);
            set => this.SetValue(ContentProperty, value);
        }

        /// <summary>
        ///     Gets or sets the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.CornerRadius" />.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets or sets the mode for the effect of the <see cref="T:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl" />.
        /// </summary>
        public FluentControlEffectMode EffectMode
        {
            get => (FluentControlEffectMode) this.GetValue(EffectModeProperty);
            set => this.SetValue(EffectModeProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the fluentControl should be highlighted (imitating IsMouseOver).
        ///     This property is intended for and works when the control is nested in a ButtonBase-inheriting class.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsHighlighted
        {
            get => (bool) this.GetValue(IsHighlightedProperty);
            set => this.SetValue(IsHighlightedProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control is pressed.
        /// </summary>
        public bool IsPressed
        {
            get => (bool) this.GetValue(IsPressedProperty);
            set => this.SetValue(IsPressedProperty, value);
        }

        /// <summary>
        ///     Gets or sets whether the Ripple is enabled.
        /// </summary>
        public bool IsRippleEnabled
        {
            get => (bool) this.GetValue(IsRippleEnabledProperty);
            set => this.SetValue(IsRippleEnabledProperty, value);
        }

        /// <summary>
        ///     Gets or sets whether the control should be clipped with regard to its
        ///     <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.CornerRadius" />.
        /// </summary>
        public bool IsSmartClipped
        {
            get => (bool) this.GetValue(IsSmartClippedProperty);
            set => this.SetValue(IsSmartClippedProperty, value);
        }

        /// <summary>
        ///     Gets or sets a Brush for the Ripple.
        /// </summary>
        public Brush RippleBrush
        {
            get => (Brush) this.GetValue(RippleBrushProperty);
            set => this.SetValue(RippleBrushProperty, value);
        }

        /// <summary>
        ///     Gets the calculated maximum size of the Ripple.
        /// </summary>
        public double RippleSize
        {
            get => (double) this.GetValue(RippleSizeProperty);
            internal set => this.SetValue(RippleSizePropertyKey, value);
        }

        /// <summary>
        ///     Gets the relative X position of the center of the Ripple.
        /// </summary>
        public double RippleX
        {
            get => (double) this.GetValue(RippleXProperty);
            internal set => this.SetValue(RippleXPropertyKey, value);
        }

        /// <summary>
        ///     Gets the relative Y position of the center of the Ripple.
        /// </summary>
        public double RippleY
        {
            get => (double) this.GetValue(RippleYProperty);
            internal set => this.SetValue(RippleYPropertyKey, value);
        }

        internal bool IsMouseLeftButtonDown { get; set; }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, TemplateStateNormal, false);
        }

        /// <inheritdoc />
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (this.IsRippleEnabled)
            {
                VisualStateManager.GoToState(this, TemplateStateMouseIn, true);
            }
        }

        /// <summary>
        ///     Override for the Control OnMouseLeave event handler.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="T:System.Windows.Input.MouseEventArgs" /> that
        ///     contains the event data.
        /// </param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (!this.IsPressed && !this.IsMouseLeftButtonDown)
            {
                this.IsPressed = false;
                VisualStateManager.GoToState(this, TemplateStateNormal, true);
            }
            else if (this.IsMouseLeftButtonDown)
            {
                var position = Mouse.PrimaryDevice.GetPosition(this);
                if (position.X <= 0.0 || position.X >= this.ActualWidth || position.Y <= 0.0 || position.Y >= this.ActualHeight)
                {
                    VisualStateManager.GoToState(this, TemplateStateMouseOut, true);
                    this.IsPressed = false;
                }
            }

            base.OnMouseLeave(e);
        }

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            this.RippleX = position.X - this.RippleSize / 2.0;
            this.RippleY = position.Y - this.RippleSize / 2.0;
            if (this.BorderGradient is RadialGradientBrush radialGradientBrush)
            {
                radialGradientBrush.GradientOrigin = radialGradientBrush.Center = new Point(position.X / this.ActualWidth, position.Y / this.ActualHeight);
            }

            base.OnMouseMove(e);
        }


        /// <inheritdoc />
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.IsMouseLeftButtonDown = true;
            if (this.IsRippleEnabled)
            {
                VisualStateManager.GoToState(this, TemplateStateMousePressed, true);
            }

            PressedInstances.Add(this);
            if (!this.IsPressed)
            {
                this.IsPressed = true;
            }
        }

        /// <summary>
        ///     Override for the Control OnRenderSizeChanged event handler.
        /// </summary>
        /// <param name="sizeInfo">Details of the old and new size involved in the change.</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            this.HandleRenderSizeChanged();
            base.OnRenderSizeChanged(sizeInfo);
        }

        private void ApplySmartClip()
        {
            var rect = new Rect(this.RenderSize);
            var rect2 = CalculateRectIncludedBorderThickness(rect, new Thickness(0.0));
            if (rect.Size.Height > 0.0 && rect.Size.Width > 0.0)
            {
                this.Clip = GenerateClipGeometry(rect2, new Radiuses(this.CornerRadius, this.BorderThickness));
            }
        }

        /// <summary>
        ///     Method called for each of the controls in pressedInstanced HashSet when mouse is moved over a
        ///     <see cref="T:System.Windows.Controls.Primitives.ButtonBase" />.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="T:System.Windows.Input.MouseEventArgs" /> from the MouseMove
        ///     <see cref="T:System.Windows.Controls.Primitives.ButtonBase" /> class event handler.
        /// </param>
        private void HandleMouseMoveExternal(MouseEventArgs e)
        {
            this.OnMouseMove(e);
            var position = Mouse.GetPosition(this);
            if (position.X >= 0.0 && position.Y >= 0.0 && position.X < this.ActualWidth && position.Y < this.ActualHeight)
            {
                if (this.IsMouseLeftButtonDown)
                {
                    this.RemoveHighlight();
                    if (!this.IsPressed)
                    {
                        this.SetIsPressedInMouseMoveExternal();
                        this.IsPressed = true;
                    }
                }

                return;
            }

            VisualStateManager.GoToState(this, TemplateStateMouseOut, true);
            this.IsPressed = false;
            if (!this.IsMouseLeftButtonDown)
            {
                PressedInstances.Remove(this);
                return;
            }

            this.SetHighlight();
        }

        /// <summary>
        ///     Method called for each of the controls in pressedInstanced HashSet.
        ///     Called before falsifying the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.FluentControl.IsPressed" />
        ///     and isMouseLeftButtonDown, and the RemoveHighlight method.
        /// </summary>
        private void HandlePreviewMouseButtonUpAttached()
        {
            var position = Mouse.PrimaryDevice.GetPosition(this);
            var flag = position.X > 0.0 && position.X < this.ActualWidth && position.Y > 0.0 && position.Y < this.ActualHeight;
            if (flag && this.IsRippleEnabled)
            {
                VisualStateManager.GoToState(this, TemplateStateMouseIn, true);
                return;
            }

            VisualStateManager.GoToState(this, TemplateStateNormal, true);
        }

        private void HandleRenderSizeChanged()
        {
            this.RippleSize = this.RenderSize.Width * 2.0;
            if (this.IsSmartClipped)
            {
                this.ApplySmartClip();
            }
        }

        private void RemoveHighlight()
        {
            if (this.IsHighlighted)
            {
                this.IsHighlighted = false;
            }
        }

        private void SetHighlight()
        {
            this.IsHighlighted = true;
        }

        private void SetIsPressedInMouseMoveExternal()
        {
            if (this.IsRippleEnabled)
            {
                VisualStateManager.GoToState(this, TemplateStateMouseIn, true);
            }
        }

        internal static Geometry GenerateClipGeometry(Rect rect, Radiuses radiuses)
        {
            var streamGeometry = new StreamGeometry();
            using (var streamGeometryContext = streamGeometry.Open())
            {
                var point = new Point(radiuses.LeftTop, 0.0);
                var point2 = new Point(rect.Width - radiuses.RightTop, 0.0);
                var point3 = new Point(rect.Width, radiuses.TopRight);
                var point4 = new Point(rect.Width, rect.Height - radiuses.BottomRight);
                var point5 = new Point(rect.Width - radiuses.RightBottom, rect.Height);
                var point6 = new Point(radiuses.LeftBottom, rect.Height);
                var point7 = new Point(0.0, rect.Height - radiuses.BottomLeft);
                var point8 = new Point(0.0, radiuses.TopLeft);
                if (point.X > point2.X)
                {
                    var x = radiuses.LeftTop / (radiuses.LeftTop + radiuses.RightTop) * rect.Width;
                    point.X = x;
                    point2.X = x;
                }

                if (point3.Y > point4.Y)
                {
                    var y = radiuses.TopRight / (radiuses.TopRight + radiuses.BottomRight) * rect.Height;
                    point3.Y = y;
                    point4.Y = y;
                }

                if (point5.X < point6.X)
                {
                    var x2 = radiuses.LeftBottom / (radiuses.LeftBottom + radiuses.RightBottom) * rect.Width;
                    point5.X = x2;
                    point6.X = x2;
                }

                if (point7.Y < point8.Y)
                {
                    var y2 = radiuses.TopLeft / (radiuses.TopLeft + radiuses.BottomLeft) * rect.Height;
                    point7.Y = y2;
                    point8.Y = y2;
                }

                var vector = new Vector(rect.TopLeft.X, rect.TopLeft.Y);
                point += vector;
                point2 += vector;
                point3 += vector;
                point4 += vector;
                point5 += vector;
                point6 += vector;
                point7 += vector;
                point8 += vector;
                streamGeometryContext.BeginFigure(point, true, true);
                streamGeometryContext.LineTo(point2, true, false);
                var num = rect.TopRight.X - point2.X;
                var num2 = point3.Y - rect.TopRight.Y;
                if (!MathUtilities.IsCloseToZero(num) || !MathUtilities.IsCloseToZero(num2))
                {
                    streamGeometryContext.ArcTo(point3, new Size(num, num2), 0.0, false, SweepDirection.Clockwise, true, false);
                }

                streamGeometryContext.LineTo(point4, true, false);
                num = rect.BottomRight.X - point5.X;
                num2 = rect.BottomRight.Y - point4.Y;
                if (!MathUtilities.IsCloseToZero(num) || !MathUtilities.IsCloseToZero(num2))
                {
                    streamGeometryContext.ArcTo(point5, new Size(num, num2), 0.0, false, SweepDirection.Clockwise, true, false);
                }

                streamGeometryContext.LineTo(point6, true, false);
                num = point6.X - rect.BottomLeft.X;
                num2 = rect.BottomLeft.Y - point7.Y;
                if (!MathUtilities.IsCloseToZero(num) || !MathUtilities.IsCloseToZero(num2))
                {
                    streamGeometryContext.ArcTo(point7, new Size(num, num2), 0.0, false, SweepDirection.Clockwise, true, false);
                }

                streamGeometryContext.LineTo(point8, true, false);
                num = point.X - rect.TopLeft.X;
                num2 = point8.Y - rect.TopLeft.Y;
                if (!MathUtilities.IsCloseToZero(num) || !MathUtilities.IsCloseToZero(num2))
                {
                    streamGeometryContext.ArcTo(point, new Size(num, num2), 0.0, false, SweepDirection.Clockwise, true, false);
                }
            }

            return streamGeometry;
        }

        private static void ButtonBaseMouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            foreach (var fluentControl in PressedInstances.ToList())
            {
                fluentControl.HandleMouseMoveExternal(e);
            }
        }

        private static Rect CalculateRectIncludedBorderThickness(Rect rect, Thickness borderThickness)
            => new Rect(rect.Left + borderThickness.Left, 
                        rect.Top + borderThickness.Top, 
                        Math.Max(0.0, rect.Width - borderThickness.Left - borderThickness.Right), 
                        Math.Max(0.0, rect.Height - borderThickness.Top - borderThickness.Bottom));

        private static object OnBorderGradientPropertyCoerce(DependencyObject d, object baseValue)
        {
            if (baseValue is Brush brush && brush.IsFrozen)
            {
                return brush.Clone();
            }

            return baseValue;
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FluentControl fluentControl && fluentControl.IsSmartClipped && fluentControl.IsInitialized)
            {
                fluentControl.ApplySmartClip();
            }
        }

        private static void OnIsRippleEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue && d is FluentControl fluentControl)
            {
                VisualStateManager.GoToState(fluentControl, TemplateStateNormal, false);
            }
        }

        private static void OnIsSmartClippedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FluentControl fluentControl))
            {
                return;
            }

            if ((bool) e.NewValue)
            {
                fluentControl.ApplySmartClip();
                return;
            }

            fluentControl.ClearValue(ClipProperty);
        }

        private static void PreviewMouseButtonUpEventHandler(object sender, MouseButtonEventArgs e)
        {
            foreach (var fluentControl in PressedInstances)
            {
                fluentControl.HandlePreviewMouseButtonUpAttached();
                if (e.ButtonState == MouseButtonState.Released)
                {
                    fluentControl.IsPressed = false;
                    fluentControl.IsMouseLeftButtonDown = false;
                    fluentControl.RemoveHighlight();
                }
            }

            PressedInstances.Clear();
        }

        internal struct Radiuses
        {
            internal double BottomLeft;

            internal double BottomRight;

            internal double LeftBottom;

            internal double LeftTop;

            internal double RightBottom;

            internal double RightTop;

            internal double TopLeft;

            internal double TopRight;

            internal Radiuses(CornerRadius cornerRadius, Thickness borders)
            {
                var num = 0.5 * borders.Left;
                var num2 = 0.5 * borders.Top;
                var num3 = 0.5 * borders.Right;
                var num4 = 0.5 * borders.Bottom;
                this.LeftTop = Math.Max(0.0, cornerRadius.TopLeft - num);
                this.TopLeft = Math.Max(0.0, cornerRadius.TopLeft - num2);
                this.TopRight = Math.Max(0.0, cornerRadius.TopRight - num2);
                this.RightTop = Math.Max(0.0, cornerRadius.TopRight - num3);
                this.RightBottom = Math.Max(0.0, cornerRadius.BottomRight - num3);
                this.BottomRight = Math.Max(0.0, cornerRadius.BottomRight - num4);
                this.BottomLeft = Math.Max(0.0, cornerRadius.BottomLeft - num4);
                this.LeftBottom = Math.Max(0.0, cornerRadius.BottomLeft - num);
            }
        }
    }
}