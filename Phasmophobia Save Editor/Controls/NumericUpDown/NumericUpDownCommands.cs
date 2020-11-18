using System.Windows.Input;

namespace PhasmophobiaSaveEditor.Controls
{
    internal class NumericUpDownCommands
    {
        public static RoutedUICommand DecreaseCommand { get; } = new RoutedUICommand("Decrease value", nameof(DecreaseCommand), typeof(NumericUpDownCommands));
        public static RoutedUICommand IncreaseCommand { get; } = new RoutedUICommand("Increase value", nameof(IncreaseCommand), typeof(NumericUpDownCommands));
    }
}