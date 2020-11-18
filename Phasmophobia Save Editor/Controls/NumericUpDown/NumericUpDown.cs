using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhasmophobiaSaveEditor.Controls
{
    internal class NumericUpDown : Control
    {
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(nameof(MaxValue), typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MaxValue));

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(nameof(MinValue), typeof(int), typeof(NumericUpDown), new PropertyMetadata(int.MinValue));

        public static readonly DependencyProperty ShowTextBoxProperty =
            DependencyProperty.Register(nameof(ShowTextBox), typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));

        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register(nameof(Step), typeof(int), typeof(NumericUpDown), new PropertyMetadata(1));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal, OnValueChanged, OnCoerceValue));

        public static readonly DependencyProperty ShowButtonsProperty =
            DependencyProperty.Register(nameof(ShowButtons), typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));


        private TextBox textBox;


        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new CommandBinding(NumericUpDownCommands.IncreaseCommand, IncreaseCommandExecute, CanIncreaseCommandExecute));
            CommandManager.RegisterClassCommandBinding(typeof(NumericUpDown), new CommandBinding(NumericUpDownCommands.DecreaseCommand, DecreaseCommandExecute, CanDecreaseCommandExecute));
        }

        public bool IsReadOnly
        {
            get => (bool) this.GetValue(IsReadOnlyProperty);
            set => this.SetValue(IsReadOnlyProperty, value);
        }

        public int MaxValue
        {
            get => (int) this.GetValue(MaxValueProperty);
            set => this.SetValue(MaxValueProperty, value);
        }

        public int MinValue
        {
            get => (int) this.GetValue(MinValueProperty);
            set => this.SetValue(MinValueProperty, value);
        }

        public bool ShowButtons
        {
            get => (bool) this.GetValue(ShowButtonsProperty);
            set => this.SetValue(ShowButtonsProperty, value);
        }

        public bool ShowTextBox
        {
            get => (bool) this.GetValue(ShowTextBoxProperty);
            set => this.SetValue(ShowTextBoxProperty, value);
        }

        public int Step
        {
            get => (int) this.GetValue(StepProperty);
            set => this.SetValue(StepProperty, value);
        }

        public int Value
        {
            get => (int) this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        private static void CanDecreaseCommandExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = sender is NumericUpDown;
        }

        private static void CanIncreaseCommandExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = sender is NumericUpDown;
        }

        private static void DecreaseCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NumericUpDown numeric)
            {
                numeric.ChangeValue(-numeric.Step);
            }
        }

        private static int GetValueInRange(NumericUpDown numeric, int value)
        {
            var minimum = numeric.MinValue;
            var maximum = numeric.MaxValue;
            if (value < minimum || minimum > maximum)
            {
                return minimum;
            }

            return value > maximum ? maximum : value;
        }

        private static void IncreaseCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is NumericUpDown numeric)
            {
                numeric.ChangeValue(numeric.Step);
            }
        }

        private static object OnCoerceValue(DependencyObject d, object value)
        {
            var numeric = (NumericUpDown) d;
            return value != null ? GetValueInRange(numeric, (int) value) : value;
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.textBox != null)
            {
                this.textBox.PreviewMouseWheel -= this.OnTextBoxPreviewMouseWheel;
            }

            this.textBox = this.GetTemplateChild("textbox") as TextBox;
            if (this.textBox != null)
            {
                this.textBox.PreviewMouseWheel += this.OnTextBoxPreviewMouseWheel;
            }
        }

        private void ChangeValue(int value)
        {
            this.SetCurrentValue(ValueProperty, this.Value + value);
        }

        private void OnTextBoxPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (this.textBox.IsKeyboardFocused)
            {
                this.ChangeValue(e.Delta > 0 ? this.Step : -this.Step);
                e.Handled = true;
            }
        }
    }
}