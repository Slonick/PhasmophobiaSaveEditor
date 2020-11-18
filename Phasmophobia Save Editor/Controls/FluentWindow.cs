using System.Windows;
using System.Windows.Media;
using PhasmophobiaSaveEditor.Utils;

namespace PhasmophobiaSaveEditor.Controls
{
    public class FluentWindow : STWindow
    {
        public static readonly DependencyProperty FluentOpacityProperty =
            DependencyProperty.Register(nameof(FluentOpacity), typeof(uint), typeof(FluentWindow), new FrameworkPropertyMetadata(200u, OnFluentOpacityChanged, CoerceFluentOpacity), ValidateFluentOpacity);

        public static readonly DependencyProperty FluentBackgroundProperty =
            DependencyProperty.Register(nameof(FluentBackground), typeof(SolidColorBrush), typeof(FluentWindow), new PropertyMetadata(default(SolidColorBrush), OnFluentOpacityChanged));

        static FluentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentWindow), new FrameworkPropertyMetadata(typeof(FluentWindow)));
        }

        public FluentWindow()
        {
            WindowResizer.ApplyToWindow(this);
        }

        public SolidColorBrush FluentBackground
        {
            get => (SolidColorBrush) this.GetValue(FluentBackgroundProperty);
            set => this.SetValue(FluentBackgroundProperty, value);
        }

        public uint FluentOpacity
        {
            get => (uint) this.GetValue(FluentOpacityProperty);
            set => this.SetValue(FluentOpacityProperty, value);
        }

        protected override void OnLoaded(Window sender)
        {
            AcrylicHelper.EnableBlur(this, this.FluentOpacity);
        }

        private static object CoerceFluentOpacity(DependencyObject d, object baseValue)
        {
            if (d is FluentWindow window && baseValue is uint opacity && opacity > 255)
            {
                return 255;
            }

            return baseValue;
        }

        private static void OnFluentOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FluentWindow window && window.IsLoaded)
            {
                AcrylicHelper.EnableBlur(window, window.FluentOpacity);
            }
        }

        private static bool ValidateFluentOpacity(object value)
        {
            if (value is uint opacity)
            {
                return opacity <= 255;
            }

            return false;
        }
    }
}