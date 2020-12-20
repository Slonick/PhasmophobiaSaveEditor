using System;
using System.Windows;
using System.Windows.Input;
using PhasmophobiaSaveEditor.ViewModels.Base;

namespace PhasmophobiaSaveEditor.Controls
{
    public class ExceptionWindow : FluentWindow
    {
        /// <summary>Identifies the <see cref="Exception"/> dependency property.</summary>
        public static readonly DependencyProperty ExceptionProperty =
            DependencyProperty.Register(nameof(Exception), typeof(Exception), typeof(ExceptionWindow), new PropertyMetadata(default(Exception)));

        public ExceptionWindow()
        {
            this.ContinueCommand = new RelayCommand(this.Close);
            this.QuitCommand = new RelayCommand(() => Environment.Exit(this.Exception.HResult));
        }

        static ExceptionWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExceptionWindow), new FrameworkPropertyMetadata(typeof(ExceptionWindow)));
        }

        public ICommand ContinueCommand { get; }
        public ICommand QuitCommand { get; }

        public Exception Exception
        {
            get => (Exception) this.GetValue(ExceptionProperty);
            set => this.SetValue(ExceptionProperty, value);
        }
    }
}