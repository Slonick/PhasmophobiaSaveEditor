using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using PhasmophobiaSaveEditor.Extensions;

namespace PhasmophobiaSaveEditor.Controls
{
    public class SplitButton : ContentControl, ICommandSource
    {
        /// <summary>Identifies the <see cref="ButtonPartStyle"/> dependency property.</summary>
        public static readonly DependencyProperty ButtonPartStyleProperty =
            DependencyProperty.Register(nameof(ButtonPartStyle), typeof(Style), typeof(SplitButton), null);

        /// <summary>Identifies the <see cref="CloseOnPopupMouseLeftButtonUp"/> dependency property.</summary>
        public static readonly DependencyProperty CloseOnPopupMouseLeftButtonUpProperty =
            DependencyProperty.Register(nameof(CloseOnPopupMouseLeftButtonUp), typeof(bool), typeof(SplitButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="CommandParameter"/> dependency property.</summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(SplitButton), new PropertyMetadata(OnCommandParameterChanged));

        /// <summary>Identifies the <see cref="Command"/> dependency property.</summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(SplitButton), new PropertyMetadata(OnCommandChanged));

        /// <summary>Identifies the <see cref="CommandTarget"/> dependency property.</summary>
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register(nameof(CommandTarget), typeof(IInputElement), typeof(SplitButton), new PropertyMetadata(OnCommandTargetChanged));

        /// <summary>Identifies the <see cref="DropDownButtonPosition"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownButtonPositionProperty =
            DependencyProperty.Register(nameof(DropDownButtonPosition), typeof(DropDownButtonPosition), typeof(SplitButton), new PropertyMetadata(DropDownButtonPosition.Right));

        /// <summary>Identifies the <see cref="DropDownContent"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContentProperty =
            DependencyProperty.Register(nameof(DropDownContent), typeof(object), typeof(SplitButton), new PropertyMetadata(null));

        /// <summary>Identifies the <see cref="DropDownContentTemplate"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContentTemplateProperty =
            DependencyProperty.Register(nameof(DropDownContentTemplate), typeof(DataTemplate), typeof(SplitButton));

        /// <summary>Identifies the <see cref="DropDownContentTemplateSelector"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContentTemplateSelectorProperty =
            DependencyProperty.Register(nameof(DropDownContentTemplateSelector), typeof(DataTemplateSelector), typeof(SplitButton), new PropertyMetadata(OnDropDownContentTemplateSelectorChanged));

        /// <summary>Identifies the <see cref="DropDownHeight"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownHeightProperty =
            DependencyProperty.Register(nameof(DropDownHeight), typeof(double), typeof(SplitButton), new PropertyMetadata(double.NaN));

        /// <summary>Identifies the <see cref="DropDownIndicatorVisibility"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownIndicatorVisibilityProperty =
            DependencyProperty.Register(nameof(DropDownIndicatorVisibility), typeof(Visibility), typeof(SplitButton), new PropertyMetadata(Visibility.Visible));

        /// <summary>Identifies the <see cref="DropDownMaxHeight"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownMaxHeightProperty =
            DependencyProperty.Register(nameof(DropDownMaxHeight), typeof(double), typeof(SplitButton), new PropertyMetadata(double.PositiveInfinity));

        /// <summary>Identifies the <see cref="DropDownMaxWidth"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownMaxWidthProperty =
            DependencyProperty.Register(nameof(DropDownMaxWidth), typeof(double), typeof(SplitButton), new PropertyMetadata(double.PositiveInfinity));

        /// <summary>Identifies the <see cref="DropDownPlacement"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownPlacementProperty =
            DependencyProperty.Register(nameof(DropDownPlacement), typeof(PlacementMode), typeof(SplitButton), new PropertyMetadata(PlacementMode.Bottom, OnDropDownPlacementChanged));

        /// <summary>Identifies the <see cref="DropDownWidth"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownWidthProperty =
            DependencyProperty.Register(nameof(DropDownWidth), typeof(double), typeof(SplitButton), new PropertyMetadata(double.NaN));

        /// <summary>Identifies the <see cref="IsButtonPartVisible"/> dependency property.</summary>
        public static readonly DependencyProperty IsButtonPartVisibleProperty =
            DependencyProperty.Register(nameof(IsButtonPartVisible), typeof(bool), typeof(SplitButton), new PropertyMetadata(true));

        /// <summary>Identifies the <see cref="IsChecked"/> dependency property.</summary>
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(SplitButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(SplitButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="IsToggle"/> dependency property.</summary>
        public static readonly DependencyProperty IsToggleProperty =
            DependencyProperty.Register(nameof(IsToggle), typeof(bool), typeof(SplitButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="KeepOpen"/> dependency property.</summary>
        public static readonly DependencyProperty KeepOpenProperty =
            DependencyProperty.Register(nameof(KeepOpen), typeof(bool), typeof(SplitButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="PopupAnimation"/> dependency property.</summary>
        public static readonly DependencyProperty PopupAnimationProperty =
            DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(SplitButton), new PropertyMetadata(PopupAnimation.None));

        /// <summary>
        ///     Identifies the ToggleContentRotateAngle dependency property.
        /// </summary>
        public static readonly DependencyProperty ToggleContentRotateAngleProperty =
            DependencyProperty.RegisterAttached("ToggleContentRotateAngle", typeof(double), typeof(SplitButton), new PropertyMetadata(0.0));

        /// <summary>Identifies the <see cref="TogglePartStyle"/> dependency property.</summary>
        public static readonly DependencyProperty TogglePartStyleProperty =
            DependencyProperty.Register(nameof(TogglePartStyle), typeof(Style), typeof(SplitButton), null);

        private bool canExecute = true;

        private Path dropDownIndicator;
        private EventHandler guardCanExecuteChangedHandler;

        private bool isTemplateApplyed;

        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
        }

        /// <summary>
        ///     Gets or sets the style for the Button used by the SplitButton.
        /// </summary>
        public Style ButtonPartStyle
        {
            get => (Style) this.GetValue(ButtonPartStyleProperty);
            set => this.SetValue(ButtonPartStyleProperty, value);
        }

        /// <summary>
        ///     Gets or sets whether the Popup should be closed when the user clicks on the DropDownContent.
        ///     The closing is executed on MouseLeftButtonUp event of the Popup.
        ///     This is a dependency property.
        /// </summary>
        public bool CloseOnPopupMouseLeftButtonUp
        {
            get => (bool) this.GetValue(CloseOnPopupMouseLeftButtonUpProperty);
            set => this.SetValue(CloseOnPopupMouseLeftButtonUpProperty, value);
        }

        /// <summary>
        ///     Gets the command that will be executed when the command source is invoked.
        ///     This is a dependency property.
        /// </summary>
        [TypeConverter(typeof(CommandConverter))]
        public ICommand Command
        {
            get => (ICommand) this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Represents a user defined data value that can be passed to the command when it is executed.
        ///     This is a dependency property.
        /// </summary>
        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        ///     The object that the command is being executed on.
        ///     This is a dependency property.
        /// </summary>
        public IInputElement CommandTarget
        {
            get => (IInputElement) this.GetValue(CommandTargetProperty);
            set => this.SetValue(CommandTargetProperty, value);
        }

        /// <summary>
        ///     Gets or sets the position of the drop down indicator.
        ///     This is a dependency property.
        /// </summary>
        public DropDownButtonPosition DropDownButtonPosition
        {
            get => (DropDownButtonPosition) this.GetValue(DropDownButtonPositionProperty);
            set => this.SetValue(DropDownButtonPositionProperty, value);
        }

        /// <summary>
        ///     Gets or sets a content to popup.
        ///     This is a dependency property.
        /// </summary>
        public object DropDownContent
        {
            get => this.GetValue(DropDownContentProperty);
            set => this.SetValue(DropDownContentProperty, value);
        }

        /// <summary>
        ///     Gets or sets the template used to display the drop-down content of the button.
        ///     This is a dependency property.
        /// </summary>
        public DataTemplate DropDownContentTemplate
        {
            get => (DataTemplate) this.GetValue(DropDownContentTemplateProperty);
            set => this.SetValue(DropDownContentTemplateProperty, value);
        }

        /// <summary>
        ///     Gets or sets the template used to display the drop-down content of the button.
        ///     This is a dependency property.
        /// </summary>
        public DataTemplateSelector DropDownContentTemplateSelector
        {
            get => (DataTemplateSelector) this.GetValue(DropDownContentTemplateSelectorProperty);
            set => this.SetValue(DropDownContentTemplateSelectorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the Height of the popup.
        ///     This is a dependency property.
        /// </summary>
        public double DropDownHeight
        {
            get => (double) this.GetValue(DropDownHeightProperty);
            set => this.SetValue(DropDownHeightProperty, value);
        }

        /// <summary>
        ///     Gets or sets whether the DropDown indicator visibility.
        ///     This is a dependency property.
        /// </summary>
        public Visibility DropDownIndicatorVisibility
        {
            get => (Visibility) this.GetValue(DropDownIndicatorVisibilityProperty);
            set => this.SetValue(DropDownIndicatorVisibilityProperty, value);
        }

        /// <summary>
        ///     Gets or sets the MaxHeight of the popup.
        ///     This is a dependency property.
        /// </summary>
        public double DropDownMaxHeight
        {
            get => (double) this.GetValue(DropDownMaxHeightProperty);
            set => this.SetValue(DropDownMaxHeightProperty, value);
        }

        /// <summary>
        ///     Gets or sets the MaxWidth of the popup.
        ///     This is a dependency property.
        /// </summary>
        public double DropDownMaxWidth
        {
            get => (double) this.GetValue(DropDownMaxWidthProperty);
            set => this.SetValue(DropDownMaxWidthProperty, value);
        }

        /// <summary>
        ///     Gets or sets the position of the popup.
        ///     This is a dependency property.
        /// </summary>
        public PlacementMode DropDownPlacement
        {
            get => (PlacementMode) this.GetValue(DropDownPlacementProperty);
            set => this.SetValue(DropDownPlacementProperty, value);
        }

        /// <summary>
        ///     Gets or sets the Width of the popup.
        ///     This is a dependency property.
        /// </summary>
        public double DropDownWidth
        {
            get => (double) this.GetValue(DropDownWidthProperty);
            set => this.SetValue(DropDownWidthProperty, value);
        }

        /// <summary>
        ///     Gets or sets whether the button part is visible.
        /// </summary>
        public bool IsButtonPartVisible
        {
            get => (bool) this.GetValue(IsButtonPartVisibleProperty);
            set => this.SetValue(IsButtonPartVisibleProperty, value);
        }

        /// <summary>
        ///     Simulates the IsChecked of the ToggleButton.
        ///     This is a dependency property.
        /// </summary>
        public bool IsChecked
        {
            get => this.IsToggle && (bool) this.GetValue(IsCheckedProperty);
            set => this.SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        ///     Gets or sets whether the popup window is expanded.
        ///     This is a dependency property.
        /// </summary>
        public bool IsOpen
        {
            get => (bool) this.GetValue(IsOpenProperty);
            set => this.SetValue(IsOpenProperty, value);
        }

        /// <summary>
        ///     Gets or sets whether the popup supports toggle mode.
        ///     This is a dependency property.
        /// </summary>
        public bool IsToggle
        {
            get => (bool) this.GetValue(IsToggleProperty);
            set => this.SetValue(IsToggleProperty, value);
        }

        public bool KeepOpen
        {
            get => (bool) this.GetValue(KeepOpenProperty);
            set => this.SetValue(KeepOpenProperty, value);
        }

        public PopupAnimation PopupAnimation
        {
            get => (PopupAnimation) this.GetValue(PopupAnimationProperty);
            set => this.SetValue(PopupAnimationProperty, value);
        }

        /// <summary>
        ///     Gets or sets the style for the ToggleButton used by the SplitButton.
        /// </summary>
        public Style TogglePartStyle
        {
            get => (Style) this.GetValue(TogglePartStyleProperty);
            set => this.SetValue(TogglePartStyleProperty, value);
        }

        /// <summary>
        ///     Gets a value that becomes the return value of <see cref="P:System.Windows.UIElement.IsEnabled"/> in derived
        ///     classes.
        /// </summary>
        /// <returns>true if the element is enabled; otherwise, false.</returns>
        protected override bool IsEnabledCore
        {
            get => base.IsEnabledCore && this.canExecute;
        }

        internal Button ActionButton { get; set; }

        internal ToggleButton DropDownButton { get; set; }

        internal Popup DropDownPopup { get; set; }

        /// <summary>
        ///     Invoked whenever application code or internal processes
        ///     (such as a rebuilding layout pass) call.
        ///     <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.isTemplateApplyed = true;
            this.SetDropDownContentTemplate();
            this.InitializeElements();
            this.ApplyPopupPlacement();
            this.InitializeDropdownPopup();
        }

        internal void ApplyPopupPlacement()
        {
            this.DropDownPopup?.SetCurrentValue(Popup.PlacementProperty, this.DropDownPlacement);
            this.UpdateDropDownPlacement(this.DropDownPlacement);
        }

        internal bool IsElementInPopup(FrameworkElement element)
        {
            var popup = element?.ParentOfType<Popup>();
            return popup != null && popup.Equals(this.DropDownPopup);
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e) => this.OnClick();

        private void CanExecuteApply()
        {
            if (this.Command == null)
            {
                return;
            }

            var flag = !(this.Command is RoutedCommand routedCommand)
                ? this.Command.CanExecute(this.CommandParameter)
                : routedCommand.CanExecute(this.CommandParameter, this.CommandTarget ?? this);

            this.canExecute = flag;
            this.CoerceValue(IsEnabledProperty);
        }

        private void CanExecuteChanged(object sender, EventArgs e) => this.CanExecuteApply();

        private void CloseAllPopupsToParent(FrameworkElement element)
        {
            for (var frameworkElement = element; frameworkElement != this; frameworkElement = frameworkElement.ParentOfType<FrameworkElement>())
            {
                switch (frameworkElement)
                {
                    case SplitButton splitButton:
                        splitButton.SetCurrentValue(IsOpenProperty, false);
                        break;
                    case DropDownButton radDropDownButton:
                        radDropDownButton.SetCurrentValue(IsOpenProperty, false);
                        break;
                }
            }

            this.SetCurrentValue(IsOpenProperty, false);
        }

        private void ExecuteCommand()
        {
            if (this.Command == null)
            {
                return;
            }

            if (!(this.Command is RoutedCommand routedCommand))
            {
                this.Command.Execute(this.CommandParameter);
                return;
            }

            routedCommand.Execute(this.CommandParameter, this.CommandTarget ?? this);
        }

        private void InitializeDropdownPopup()
        {
            this.DropDownPopup?.RemoveHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnPopupMouseUp));

            var templateChild = this.GetTemplateChild(nameof(this.DropDownPopup));
            this.DropDownPopup = templateChild as Popup;
            if (this.DropDownPopup == null)
            {
                return;
            }

            this.DropDownPopup.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnPopupMouseUp), true);
            this.DropDownPopup.SetCurrentValue(Popup.StaysOpenProperty, this.KeepOpen);
        }

        private void InitializeElements()
        {
            if (this.ActionButton != null)
            {
                this.ActionButton.Click -= this.ActionButton_Click;
            }

            this.ActionButton = this.GetTemplateChild("ButtonPart") as Button;
            this.DropDownButton = this.GetTemplateChild("DropDownPart") as ToggleButton;
            this.dropDownIndicator = this.GetTemplateChild("PART_DropDownIndicator") as Path;
            if (this.ActionButton != null)
            {
                this.ActionButton.Click += this.ActionButton_Click;
            }
        }

        /// <summary>
        ///     Toggle the IsOpen property and execute the associated Command.
        /// </summary>
        private void OnClick()
        {
            this.SetCurrentValue(IsOpenProperty, false);
            this.OnToggle();
            this.ExecuteCommand();
        }

        private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            if (oldCommand != null)
            {
                oldCommand.CanExecuteChanged -= this.CanExecuteChanged;
            }

            if (newCommand != null)
            {
                this.guardCanExecuteChangedHandler = this.CanExecuteChanged;
                newCommand.CanExecuteChanged += this.guardCanExecuteChangedHandler;
            }

            this.CanExecuteApply();
        }

        /// <summary>
        ///     Called when the DropDownContentTemplateSelector property of a SplitButton changes.
        /// </summary>
        private void OnDropDownContentTemplateSelectorChanged() => this.SetDropDownContentTemplate();

        /// <summary>
        ///     Called when [drop down placement changed].
        /// </summary>
        private void OnDropDownPlacementChanged() => this.ApplyPopupPlacement();

        private void OnPopupMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.CloseOnPopupMouseLeftButtonUp)
            {
                return;
            }

            if (e.OriginalSource is DropDownButton)
            {
                return;
            }

            if (e.Source is SplitButton splitButton)
            {
                var flag = splitButton.IsElementInPopup((FrameworkElement) e.OriginalSource);
                if (splitButton.ActionButton != e.OriginalSource && !flag)
                {
                    return;
                }
            }

            this.CloseAllPopupsToParent(e.OriginalSource as FrameworkElement);
        }

        /// <summary>
        ///     Toggle the IsChecked property and raise the Checked/Unchecked events.
        /// </summary>
        private void OnToggle()
        {
            if (this.IsToggle)
            {
                this.SetCurrentValue(IsCheckedProperty, !this.IsChecked);
            }
        }

        private void SetDropDownContentTemplate()
        {
            if (!this.isTemplateApplyed || this.DropDownContentTemplateSelector == null)
            {
                return;
            }

            var dataTemplate = this.DropDownContentTemplateSelector.SelectTemplate(this.DropDownContent, this);
            if (dataTemplate != null)
            {
                this.SetCurrentValue(DropDownContentTemplateProperty, dataTemplate);
            }
        }

        private void SetDropDownIndicatorRotation(double newAngle)
        {
            if (this.dropDownIndicator == null)
            {
                return;
            }

            var rotateTransform = new RotateTransform();
            this.dropDownIndicator.SetCurrentValue(RenderTransformProperty, rotateTransform);
            rotateTransform.Angle = newAngle;
        }

        private void UpdateDropDownPlacement(PlacementMode newValue)
        {
            if (this.DropDownButton != null)
            {
                switch (newValue)
                {
                    case PlacementMode.Bottom:
                        SetToggleContentRotateAngle(this.DropDownButton, 0.0);
                        this.SetDropDownIndicatorRotation(0.0);
                        return;
                    case PlacementMode.Center:
                        break;
                    case PlacementMode.Right:
                        SetToggleContentRotateAngle(this.DropDownButton, -90.0);
                        this.SetDropDownIndicatorRotation(-90.0);
                        return;
                    default:
                        switch (newValue)
                        {
                            case PlacementMode.Left:
                                SetToggleContentRotateAngle(this.DropDownButton, 90.0);
                                this.SetDropDownIndicatorRotation(90.0);
                                return;
                            case PlacementMode.Top:
                                SetToggleContentRotateAngle(this.DropDownButton, 180.0);
                                this.SetDropDownIndicatorRotation(180.0);
                                break;
                            default:
                                return;
                        }

                        break;
                }
            }
        }

        /// <summary>Helper for getting <see cref="ToggleContentRotateAngleProperty"/> from <paramref name="obj"/>.</summary>
        /// <param name="obj"><see cref="DependencyObject"/> to read <see cref="ToggleContentRotateAngleProperty"/> from.</param>
        /// <returns>ToggleContentRotateAngle property value.</returns>
        public static double GetToggleContentRotateAngle(DependencyObject obj) => (double) obj.GetValue(ToggleContentRotateAngleProperty);

        /// <summary>Helper for setting <see cref="ToggleContentRotateAngleProperty"/> on <paramref name="obj"/>.</summary>
        /// <param name="obj"><see cref="DependencyObject"/> to set <see cref="ToggleContentRotateAngleProperty"/> on.</param>
        /// <param name="value">ToggleContentRotateAngle property value.</param>
        public static void SetToggleContentRotateAngle(DependencyObject obj, double value) => obj.SetValue(ToggleContentRotateAngleProperty, value);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SplitButton splitButton)
            {
                splitButton.OnCommandChanged((ICommand) e.OldValue, (ICommand) e.NewValue);
            }
        }

        private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SplitButton splitButton)
            {
                splitButton.CanExecuteApply();
            }
        }

        private static void OnCommandTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SplitButton splitButton)
            {
                splitButton.CanExecuteApply();
            }
        }

        private static void OnDropDownContentTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((SplitButton) d).OnDropDownContentTemplateSelectorChanged();

        private static void OnDropDownPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SplitButton splitButton)
            {
                splitButton.OnDropDownPlacementChanged();
            }
        }
    }
}