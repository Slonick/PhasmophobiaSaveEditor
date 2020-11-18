using System;
using System.Windows;
using System.Windows.Input;
using PhasmophobiaSaveEditor.ViewModels.Base;

namespace PhasmophobiaSaveEditor.Controls
{
    public class STWindow : Window
    {
        public static readonly DependencyProperty AdditionalContentProperty =
            DependencyProperty.Register(nameof(AdditionalContent), typeof(object), typeof(STWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty HideHeaderProperty =
            DependencyProperty.Register(nameof(HideHeader), typeof(bool), typeof(STWindow), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="ShowIcon"/> dependency property.</summary>
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register(nameof(ShowIcon), typeof(bool), typeof(STWindow), new PropertyMetadata(true));

        protected STWindow()
        {
            this.MinimizeCommand = new RelayCommand(() => SystemCommands.MinimizeWindow(this),
                                                    () => this.ResizeMode >= ResizeMode.CanMinimize);

            this.RestoreCommand = new RelayCommand(() => SystemCommands.RestoreWindow(this),
                                                   () => this.ResizeMode >= ResizeMode.CanResize);

            this.MaximizeCommand = new RelayCommand(() => SystemCommands.MaximizeWindow(this),
                                                    () => this.ResizeMode >= ResizeMode.CanResize);

            this.CloseCommand = new RelayCommand(() => SystemCommands.CloseWindow(this));

            this.Loaded += this.OnLoaded;
        }

        public ICommand CloseCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand RestoreCommand { get; }

        public object AdditionalContent
        {
            get => this.GetValue(AdditionalContentProperty);
            set => this.SetValue(AdditionalContentProperty, value);
        }

        public bool HideHeader
        {
            get => (bool) this.GetValue(HideHeaderProperty);
            set => this.SetValue(HideHeaderProperty, value);
        }

        public bool ShowIcon
        {
            get => (bool) this.GetValue(ShowIconProperty);
            set => this.SetValue(ShowIconProperty, value);
        }

        protected virtual void OnLoaded(Window sender) { }

        private void OnLoaded(object sender, EventArgs eventArgs)
        {
            this.Loaded -= this.OnLoaded;
            if (sender is Window window)
            {
                this.OnLoaded(window);
            }
        }
    }
}