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

        public static readonly DependencyProperty FluentIsEnabledProperty =
            DependencyProperty.Register(nameof(FluentIsEnabled), typeof(bool), typeof(FluentWindow), new PropertyMetadata(true, OnFluentIsEnabledPropertyChanged));


        public FluentWindow()
        {
            WindowResizer.ApplyToWindow(this);
        }

        static FluentWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FluentWindow), new FrameworkPropertyMetadata(typeof(FluentWindow)));
        }

        public SolidColorBrush FluentBackground
        {
            get => (SolidColorBrush) this.GetValue(FluentBackgroundProperty);
            set => this.SetValue(FluentBackgroundProperty, value);
        }

        public bool FluentIsEnabled
        {
            get => (bool) this.GetValue(FluentIsEnabledProperty);
            set => this.SetValue(FluentIsEnabledProperty, value);
        }

        public uint FluentOpacity
        {
            get => (uint) this.GetValue(FluentOpacityProperty);
            set => this.SetValue(FluentOpacityProperty, value);
        }

        private static object CoerceFluentOpacity(DependencyObject d, object baseValue)
        {
            if (d is FluentWindow && baseValue is uint opacity && opacity > 255)
            {
                return 255;
            }

            return baseValue;
        }

        private static void OnFluentIsEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FluentWindow window && window.IsLoaded)
            {
                window.UpdateBlur();
            }
        }

        private static void OnFluentOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FluentWindow window && window.IsLoaded)
            {
                window.UpdateBlur();
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

        public void UpdateBlur()
        {
            AcrylicHelper.EnableBlur(this, this.FluentOpacity);
        }

        protected override void OnLoaded(Window sender)
        {
            this.UpdateBlur();
        }
    }
}