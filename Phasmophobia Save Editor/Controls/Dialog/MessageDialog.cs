using System.Windows;
using System.Windows.Input;

namespace PhasmophobiaSaveEditor.Controls.Dialog
{
    public static class MessageDialog
    {
        public static bool? ShowDialog(DialogParameters parameters)
        {
            var messageDialog = new MessageDialogInternal
            {
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = parameters.DialogStartupLocation,
                Parameters = parameters,
                Owner = parameters.Owner
            };

            if (messageDialog.Owner is null && Application.Current?.MainWindow?.IsVisible == true)
            {
                messageDialog.Owner = Application.Current?.MainWindow;
            }
            else if (Application.Current?.MainWindow == null)
            {
                messageDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            if (parameters.Header == null)
            {
                messageDialog.Parameters.Header = messageDialog.Owner?.Title;
            }

            return messageDialog.ShowDialog();
        }
    }

    public class MessageDialogInternal : FluentWindow
    {
        /// <summary>Identifies the <see cref="Parameters"/> dependency property.</summary>
        public static readonly DependencyProperty ParametersProperty =
            DependencyProperty.Register(nameof(Parameters), typeof(DialogParameters), typeof(MessageDialogInternal), new PropertyMetadata(null));

        internal MessageDialogInternal() { }

        static MessageDialogInternal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageDialogInternal), new FrameworkPropertyMetadata(typeof(MessageDialogInternal)));
            ResizeModeProperty.OverrideMetadata(typeof(MessageDialogInternal), new FrameworkPropertyMetadata(ResizeMode.NoResize));
            SizeToContentProperty.OverrideMetadata(typeof(MessageDialogInternal), new FrameworkPropertyMetadata(SizeToContent.WidthAndHeight));
            MinWidthProperty.OverrideMetadata(typeof(MessageDialogInternal), new FrameworkPropertyMetadata(150d));
            MinHeightProperty.OverrideMetadata(typeof(MessageDialogInternal), new FrameworkPropertyMetadata(100d));

            CommandManager.RegisterClassCommandBinding(typeof(MessageDialogInternal), new CommandBinding(MessageDialogCommands.YesResultCommand, YesResultCommandExecute, (_, e) => e.CanExecute = true));
            CommandManager.RegisterClassCommandBinding(typeof(MessageDialogInternal), new CommandBinding(MessageDialogCommands.CancelResultCommand, CancelResultCommandExecute, (_, e) => e.CanExecute = true));
            CommandManager.RegisterClassCommandBinding(typeof(MessageDialogInternal), new CommandBinding(MessageDialogCommands.NoResultCommand, NoResultCommandExecute, (_, e) => e.CanExecute = true));
            CommandManager.RegisterClassCommandBinding(typeof(MessageDialogInternal), new CommandBinding(MessageDialogCommands.OkResultCommand, OkResultCommandExecute, (_, e) => e.CanExecute = true));
        }

        public DialogParameters Parameters
        {
            get => (DialogParameters) this.GetValue(ParametersProperty);
            set => this.SetValue(ParametersProperty, value);
        }

        private void SetResultAndClose(bool? result)
        {
            this.DialogResult = result;
            this.Close();
        }

        private static void CancelResultCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(sender is MessageDialogInternal dialog))
            {
                return;
            }

            dialog.SetResultAndClose(null);
        }

        private static void NoResultCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(sender is MessageDialogInternal dialog))
            {
                return;
            }

            dialog.SetResultAndClose(false);
        }

        private static void OkResultCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(sender is MessageDialogInternal dialog))
            {
                return;
            }

            dialog.SetResultAndClose(true);
        }

        private static void YesResultCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!(sender is MessageDialogInternal dialog))
            {
                return;
            }

            dialog.SetResultAndClose(true);
        }
    }
}