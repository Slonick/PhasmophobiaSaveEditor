using System;
using System.Windows;
using System.Windows.Media;

namespace PhasmophobiaSaveEditor.Controls.Fluent
{
    /// <summary>
    ///     Creates an inner glow effect by itself.
    /// </summary>
    public class InnerGlow : FrameworkElement
    {
        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.InnerGlow.BaseBrush" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BaseBrushProperty = DependencyProperty.Register("BaseBrush", typeof(SolidColorBrush), typeof(InnerGlow), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender, OnBaseBrushChanged));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.InnerGlow.CornerRadius" /> dependency
        ///     property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(InnerGlow), new FrameworkPropertyMetadata(new CornerRadius(0.0), FrameworkPropertyMetadataOptions.AffectsRender, OnCornerRadiusChanged));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.InnerGlow.MaximumOpacity" /> dependency
        ///     property.
        /// </summary>
        public static readonly DependencyProperty MaximumOpacityProperty = DependencyProperty.Register("MaximumOpacity", typeof(double), typeof(InnerGlow), new FrameworkPropertyMetadata(0.4, FrameworkPropertyMetadataOptions.AffectsRender, OnMaximumOpacityChanged, OnMaximumOpacityCoerce));

        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.Controls.Fluent.InnerGlow.Spread" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SpreadProperty = DependencyProperty.Register("Spread", typeof(int), typeof(InnerGlow), new FrameworkPropertyMetadata(3, FrameworkPropertyMetadataOptions.AffectsRender, OnSpreadChanged));

        private SolidColorBrush baseBrush = (SolidColorBrush) BaseBrushProperty.DefaultMetadata.DefaultValue;

        private CornerRadius cornerRadius = (CornerRadius) CornerRadiusProperty.DefaultMetadata.DefaultValue;

        private double maximumOpacity = (double) MaximumOpacityProperty.DefaultMetadata.DefaultValue;

        private int spread = (int) SpreadProperty.DefaultMetadata.DefaultValue;

        static InnerGlow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InnerGlow), new FrameworkPropertyMetadata(typeof(InnerGlow)));
        }

        /// <summary>
        ///     Gets or sets the base brush to be used when creating the effect.
        /// </summary>
        public SolidColorBrush BaseBrush
        {
            get => (SolidColorBrush) this.GetValue(BaseBrushProperty);
            set => this.SetValue(BaseBrushProperty, value);
        }

        /// <summary>
        ///     Gets or sets the CornerRadius of the element.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius) this.GetValue(CornerRadiusProperty);
            set => this.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets or sets the maximum opacity of the glow.
        /// </summary>
        public double MaximumOpacity
        {
            get => (double) this.GetValue(MaximumOpacityProperty);
            set => this.SetValue(MaximumOpacityProperty, value);
        }

        /// <summary>
        ///     Gets or sets the spread of the inner glow effect in pixels.
        /// </summary>
        public int Spread
        {
            get => (int) this.GetValue(SpreadProperty);
            set => this.SetValue(SpreadProperty, value);
        }

        internal Drawing Drawing { get; private set; }

        /// <inheritdoc />
        protected override void OnRender(DrawingContext drawingContext)
        {
            this.PrepareVisual();
            drawingContext.DrawDrawing(this.Drawing);
            base.OnRender(drawingContext);
        }

        internal void PrepareVisual()
        {
            var drawingVisual = new DrawingVisual();
            var width = this.RenderSize.Width;
            var height = this.RenderSize.Height;
            if (this.maximumOpacity <= 0.0)
            {
                this.Drawing = null;
                return;
            }

            using (var drawingContext = drawingVisual.RenderOpen())
            {
                for (var i = 0; i < this.spread; i++)
                {
                    Brush brush = this.BaseBrush.Clone();
                    brush.Opacity = this.maximumOpacity / this.spread * (this.spread - i);
                    var num = width - 2 * i;
                    var num2 = height - 2 * i;
                    if (num < 0.0 || num2 < 0.0)
                    {
                        break;
                    }

                    var rectangle = new Rect(i, i, num, num2);
                    if (!this.CornerRadius.BottomLeft.Equals(0.0))
                    {
                        var num3 = this.CornerRadius.TopLeft - i;
                        if (num3 < 0.0)
                        {
                            num3 = 0.0;
                        }

                        drawingContext.DrawRoundedRectangle(null, new Pen(brush, 1.0), rectangle, num3, num3);
                    }
                    else
                    {
                        drawingContext.DrawRectangle(null, new Pen(brush, 1.0), rectangle);
                    }
                }
            }

            this.Drawing = drawingVisual.Drawing;
        }

        private static void OnBaseBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InnerGlow innerGlow)
            {
                var solidColorBrush = (SolidColorBrush) e.NewValue;
                if (!Equals(solidColorBrush, innerGlow.baseBrush))
                {
                    innerGlow.baseBrush = solidColorBrush;
                }
            }
        }

        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InnerGlow innerGlow)
            {
                var cr = (CornerRadius) e.NewValue;
                if (!cr.Equals(innerGlow.cornerRadius))
                {
                    innerGlow.cornerRadius = cr;
                }
            }
        }

        private static void OnMaximumOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is InnerGlow innerGlow))
            {
                return;
            }

            if (!(e.NewValue is double maximumOpacity))
            {
                return;
            }

            if (!maximumOpacity.Equals(innerGlow.maximumOpacity))
            {
                innerGlow.maximumOpacity = maximumOpacity;
            }
        }

        private static object OnMaximumOpacityCoerce(DependencyObject d, object baseValue) => Math.Min(Math.Max(0.0, (double) baseValue), 1.0);

        private static void OnSpreadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is InnerGlow innerGlow))
            {
                return;
            }

            if (!(e.NewValue is int spread))
            {
                return;
            }

            if (!spread.Equals(innerGlow.spread))
            {
                innerGlow.spread = spread;
            }
        }
    }
}