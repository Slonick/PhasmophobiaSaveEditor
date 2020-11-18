using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PhasmophobiaSaveEditor.Controls.Fluent
{
    public class AcrylicPanel : ContentControl
    {
        public static readonly DependencyProperty IsEnableAcrylicProperty =
            DependencyProperty.Register(nameof(IsEnableAcrylic), typeof(bool), typeof(AcrylicPanel), new PropertyMetadata(true));

        public static readonly DependencyProperty NoiseOpacityProperty =
            DependencyProperty.Register(nameof(NoiseOpacity), typeof(double), typeof(AcrylicPanel), new PropertyMetadata(0.03));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(FrameworkElement), typeof(AcrylicPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(nameof(Target), typeof(FrameworkElement), typeof(AcrylicPanel), new PropertyMetadata(null));

        public static readonly DependencyProperty TintColorProperty =
            DependencyProperty.Register(nameof(TintColor), typeof(Color), typeof(AcrylicPanel), new PropertyMetadata(Colors.White));

        public static readonly DependencyProperty TintOpacityProperty =
            DependencyProperty.Register(nameof(TintOpacity), typeof(double), typeof(AcrylicPanel), new PropertyMetadata(0.0));

        private bool isChanged;

        public AcrylicPanel()
        {
            this.Source = this;
        }

        static AcrylicPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AcrylicPanel), new FrameworkPropertyMetadata(typeof(AcrylicPanel)));
        }

        public bool IsEnableAcrylic
        {
            get => (bool) this.GetValue(IsEnableAcrylicProperty);
            set => this.SetValue(IsEnableAcrylicProperty, value);
        }

        public double NoiseOpacity
        {
            get => (double) this.GetValue(NoiseOpacityProperty);
            set => this.SetValue(NoiseOpacityProperty, value);
        }

        public FrameworkElement Source
        {
            get => (FrameworkElement) this.GetValue(SourceProperty);
            set => this.SetValue(SourceProperty, value);
        }

        public FrameworkElement Target
        {
            get => (FrameworkElement) this.GetValue(TargetProperty);
            set => this.SetValue(TargetProperty, value);
        }

        public Color TintColor
        {
            get => (Color) this.GetValue(TintColorProperty);
            set => this.SetValue(TintColorProperty, value);
        }

        public double TintOpacity
        {
            get => (double) this.GetValue(TintOpacityProperty);
            set => this.SetValue(TintOpacityProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.GetTemplateChild("rect") is Rectangle rect)
            {
                rect.LayoutUpdated += (_, __) =>
                {
                    if (this.isChanged)
                    {
                        return;
                    }

                    this.isChanged = true;
                    BindingOperations.GetBindingExpressionBase(rect, RenderTransformProperty)?.UpdateTarget();

                    this.Dispatcher?.BeginInvoke(new Action(() => { this.isChanged = false; }), DispatcherPriority.DataBind);
                };
            }
        }
    }
}