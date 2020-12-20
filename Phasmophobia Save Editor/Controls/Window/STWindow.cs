using System;
using System.Windows;
using System.Windows.Input;
using PhasmophobiaSaveEditor.ViewModels.Base;

namespace PhasmophobiaSaveEditor.Controls
{
    public class STWindow : Window
    {
        /// <summary>Identifies the <see cref="AdditionalContent"/> dependency property.</summary>
        public static readonly DependencyProperty AdditionalContentProperty =
            DependencyProperty.Register(nameof(AdditionalContent), typeof(object), typeof(STWindow), new PropertyMetadata(null));

        /// <summary>Identifies the <see cref="HideHeader"/> dependency property.</summary>
        public static readonly DependencyProperty HideHeaderProperty =
            DependencyProperty.Register(nameof(HideHeader), typeof(bool), typeof(STWindow), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="ShowIcon"/> dependency property.</summary>
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register(nameof(ShowIcon), typeof(bool), typeof(STWindow), new PropertyMetadata(true));

        protected STWindow()
        {
            this.Loaded += this.OnLoaded;
        }

        static STWindow()
        {
            CommandManager.RegisterClassCommandBinding(typeof(STWindow), new CommandBinding(SystemCommands.CloseWindowCommand, ExecuteSystemCommand, CanExecuteSystemCommand));
            CommandManager.RegisterClassCommandBinding(typeof(STWindow), new CommandBinding(SystemCommands.MaximizeWindowCommand, ExecuteSystemCommand, CanExecuteSystemCommand));
            CommandManager.RegisterClassCommandBinding(typeof(STWindow), new CommandBinding(SystemCommands.MinimizeWindowCommand, ExecuteSystemCommand, CanExecuteSystemCommand));
            CommandManager.RegisterClassCommandBinding(typeof(STWindow), new CommandBinding(SystemCommands.RestoreWindowCommand, ExecuteSystemCommand, CanExecuteSystemCommand));
        }

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

        private static void CanExecuteSystemCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!(e.Command is RoutedCommand command) || !(sender is STWindow window))
            {
                return;
            }

            if (command.Name == SystemCommands.CloseWindowCommand.Name)
            {
                e.CanExecute = true;
            }
            else if (command.Name == SystemCommands.MaximizeWindowCommand.Name)
            {
                e.CanExecute = window.ResizeMode >= ResizeMode.CanResize;
            }
            else if (command.Name == SystemCommands.MinimizeWindowCommand.Name)
            {
                e.CanExecute = window.ResizeMode >= ResizeMode.CanResize;
            }
            else if (command.Name == SystemCommands.RestoreWindowCommand.Name)
            {
                e.CanExecute = window.ResizeMode >= ResizeMode.CanResize;
            }
        }

        private static void ExecuteSystemCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(e.Command is RoutedCommand command) || !(sender is STWindow window))
            {
                return;
            }

            if (command.Name == SystemCommands.CloseWindowCommand.Name)
            {
                SystemCommands.CloseWindow(window);
            }
            else if (command.Name == SystemCommands.MaximizeWindowCommand.Name)
            {
                SystemCommands.MaximizeWindow(window);
            }
            else if (command.Name == SystemCommands.MinimizeWindowCommand.Name)
            {
                SystemCommands.MinimizeWindow(window);
            }
            else if (command.Name == SystemCommands.RestoreWindowCommand.Name)
            {
                SystemCommands.RestoreWindow(window);
            }
        }

        private void OnLoaded(object sender, EventArgs eventArgs)
        {
            this.Loaded -= this.OnLoaded;
            if (this.DataContext is IWindowViewModel vm)
            {
                vm.OnLoaded(this);
            }
        }
    }
}