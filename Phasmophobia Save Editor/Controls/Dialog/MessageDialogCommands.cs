using System.Windows.Input;

namespace PhasmophobiaSaveEditor.Controls.Dialog
{
    public class MessageDialogCommands
    {
        public static RoutedUICommand CancelResultCommand { get; } = new RoutedUICommand("Cancel", nameof(CancelResultCommand), typeof(MessageDialogCommands));
        public static RoutedUICommand NoResultCommand { get; } = new RoutedUICommand("No", nameof(NoResultCommand), typeof(MessageDialogCommands));
        public static RoutedUICommand OkResultCommand { get; } = new RoutedUICommand("OK", nameof(OkResultCommand), typeof(MessageDialogCommands));
        public static RoutedUICommand YesResultCommand { get; } = new RoutedUICommand("Yes", nameof(YesResultCommand), typeof(MessageDialogCommands));
    }
}