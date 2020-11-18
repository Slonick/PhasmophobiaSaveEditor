using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using PhasmophobiaSaveEditor.Extensions;

namespace PhasmophobiaSaveEditor.Controls
{
    public class DropDownButton : Button
    {
        /// <summary>Identifies the <see cref="AutoOpenDelay"/> dependency property.</summary>
        public static readonly DependencyProperty AutoOpenDelayProperty =
            DependencyProperty.Register(nameof(AutoOpenDelay), typeof(TimeSpan), typeof(DropDownButton), new PropertyMetadata(TimeSpan.Zero, OnAutoOpenDelayChanged));

        /// <summary>Identifies the <see cref="CloseOnEscape"/> dependency property.</summary>
        public static readonly DependencyProperty CloseOnEscapeProperty =
            DependencyProperty.Register(nameof(CloseOnEscape), typeof(bool), typeof(DropDownButton), new PropertyMetadata(true));

        /// <summary>Identifies the <see cref="DropDownContent"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContentProperty =
            DependencyProperty.Register(nameof(DropDownContent), typeof(object), typeof(DropDownButton), new PropertyMetadata(null, OnDropDownContentChanged));

        /// <summary>Identifies the <see cref="DropDownContentTemplate"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContentTemplateProperty =
            DependencyProperty.Register(nameof(DropDownContentTemplate), typeof(DataTemplate), typeof(DropDownButton), new PropertyMetadata(OnDropDownContentTemplateChanged));

        /// <summary>Identifies the <see cref="DropDownContentTemplateSelector"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContentTemplateSelectorProperty =
            DependencyProperty.Register(nameof(DropDownContentTemplateSelector), typeof(DataTemplateSelector), typeof(DropDownButton), new PropertyMetadata(OnDropDownContentTemplateSelectorChanged));

        /// <summary>Identifies the <see cref="CloseOnPopupMouseLeftButtonUp"/> dependency property.</summary>
        public static readonly DependencyProperty CloseOnPopupMouseLeftButtonUpProperty =
            DependencyProperty.Register(nameof(CloseOnPopupMouseLeftButtonUp), typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="DropDownButtonPosition"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownButtonPositionProperty =
            DependencyProperty.Register(nameof(DropDownButtonPosition), typeof(DropDownButtonPosition), typeof(DropDownButton), new PropertyMetadata(DropDownButtonPosition.Right));


        /// <summary>Identifies the <see cref="DropDownHeight"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownHeightProperty =
            DependencyProperty.Register(nameof(DropDownHeight), typeof(double), typeof(DropDownButton), new PropertyMetadata(double.NaN));

        /// <summary>Identifies the <see cref="DropDownIndicatorVisibility"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownIndicatorVisibilityProperty =
            DependencyProperty.Register(nameof(DropDownIndicatorVisibility), typeof(Visibility), typeof(DropDownButton), new PropertyMetadata(Visibility.Visible));

        /// <summary>Identifies the <see cref="DropDownMaxHeight"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownMaxHeightProperty =
            DependencyProperty.Register(nameof(DropDownMaxHeight), typeof(double), typeof(DropDownButton), new PropertyMetadata(double.PositiveInfinity));

        /// <summary>Identifies the <see cref="DropDownMaxWidth"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownMaxWidthProperty =
            DependencyProperty.Register(nameof(DropDownMaxWidth), typeof(double), typeof(DropDownButton), new PropertyMetadata(double.PositiveInfinity));

        /// <summary>Identifies the <see cref="DropDownMinHeight"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownMinHeightProperty =
            DependencyProperty.Register(nameof(DropDownMinHeight), typeof(double), typeof(DropDownButton), new PropertyMetadata(0.0));

        /// <summary>Identifies the <see cref="DropDownMinWidth"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownMinWidthProperty =
            DependencyProperty.Register(nameof(DropDownMinWidth), typeof(double), typeof(DropDownButton), new PropertyMetadata(0.0));

        /// <summary>Identifies the <see cref="DropDownPlacement"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownPlacementProperty =
            DependencyProperty.Register(nameof(DropDownPlacement), typeof(PlacementMode), typeof(DropDownButton), new PropertyMetadata(PlacementMode.Bottom));

        /// <summary>Identifies the <see cref="DropDownWidth"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownWidthProperty =
            DependencyProperty.Register(nameof(DropDownWidth), typeof(double), typeof(DropDownButton), new PropertyMetadata(double.NaN));

        /// <summary>Identifies the <see cref="IsOpen"/> dependency property.</summary>
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="KeepOpen"/> dependency property.</summary>
        public static readonly DependencyProperty KeepOpenProperty =
            DependencyProperty.Register(nameof(KeepOpen), typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));

        /// <summary>Identifies the <see cref="PopupAnimation"/> dependency property.</summary>
        public static readonly DependencyProperty PopupAnimationProperty =
            DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(DropDownButton), new PropertyMetadata(PopupAnimation.None));

        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }

        public TimeSpan AutoOpenDelay
        {
            get => (TimeSpan) this.GetValue(AutoOpenDelayProperty);
            set => this.SetValue(AutoOpenDelayProperty, value);
        }

        public bool CloseOnEscape
        {
            get => (bool) this.GetValue(CloseOnEscapeProperty);
            set => this.SetValue(CloseOnEscapeProperty, value);
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

        public DropDownButtonPosition DropDownButtonPosition
        {
            get => (DropDownButtonPosition) this.GetValue(DropDownButtonPositionProperty);
            set => this.SetValue(DropDownButtonPositionProperty, value);
        }

        public object DropDownContent
        {
            get => this.GetValue(DropDownContentProperty);
            set => this.SetValue(DropDownContentProperty, value);
        }

        public DataTemplate DropDownContentTemplate
        {
            get => (DataTemplate) this.GetValue(DropDownContentTemplateProperty);
            set => this.SetValue(DropDownContentTemplateProperty, value);
        }

        public DataTemplateSelector DropDownContentTemplateSelector
        {
            get => (DataTemplateSelector) this.GetValue(DropDownContentTemplateSelectorProperty);
            set => this.SetValue(DropDownContentTemplateSelectorProperty, value);
        }

        public double DropDownHeight
        {
            get => (double) this.GetValue(DropDownHeightProperty);
            set => this.SetValue(DropDownHeightProperty, value);
        }

        public Visibility DropDownIndicatorVisibility
        {
            get => (Visibility) this.GetValue(DropDownIndicatorVisibilityProperty);
            set => this.SetValue(DropDownIndicatorVisibilityProperty, value);
        }

        public double DropDownMaxHeight
        {
            get => (double) this.GetValue(DropDownMaxHeightProperty);
            set => this.SetValue(DropDownMaxHeightProperty, value);
        }

        public double DropDownMaxWidth
        {
            get => (double) this.GetValue(DropDownMaxWidthProperty);
            set => this.SetValue(DropDownMaxWidthProperty, value);
        }

        public double DropDownMinHeight
        {
            get => (double) this.GetValue(DropDownMinHeightProperty);
            set => this.SetValue(DropDownMinHeightProperty, value);
        }

        public double DropDownMinWidth
        {
            get => (double) this.GetValue(DropDownMinWidthProperty);
            set => this.SetValue(DropDownMinWidthProperty, value);
        }

        public PlacementMode DropDownPlacement
        {
            get => (PlacementMode) this.GetValue(DropDownPlacementProperty);
            set => this.SetValue(DropDownPlacementProperty, value);
        }

        public double DropDownWidth
        {
            get => (double) this.GetValue(DropDownWidthProperty);
            set => this.SetValue(DropDownWidthProperty, value);
        }

        public bool IsOpen
        {
            get => (bool) this.GetValue(IsOpenProperty);
            set => this.SetValue(IsOpenProperty, value);
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

        internal Popup DropDownPopup { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.InitializeDropdownPopup();
        }

        protected override void OnClick()
        {
            base.OnClick();
            if (this.IsOpen)
            {
                this.SetCurrentValue(IsOpenProperty, false);
                return;
            }

            this.SetCurrentValue(IsOpenProperty, true);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Handled)
            {
                return;
            }

            if (e.Key == Key.Escape)
            {
                if (this.IsOpen && this.CloseOnEscape && !this.KeepOpen)
                {
                    this.SetCurrentValue(IsOpenProperty, false);
                    e.Handled = true;
                    this.Focus();
                }
            }
        }

        /// <summary>
        ///     Provides class handling for the <see cref="E:System.Windows.UIElement.MouseLeftButtonDown"/>
        ///     routed event that occurs when the left mouse button is pressed while the mouse
        ///     pointer is over this control.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            //this.isUserInitiated = true;
            if (this.IsElementInPopup(e.OriginalSource as FrameworkElement))
            {
                e.Handled = true;
                return;
            }

            base.OnMouseLeftButtonDown(e);
        }

        private void CloseAllPopupsToParent(FrameworkElement element)
        {
            for (var frameworkElement = element; frameworkElement != this; frameworkElement = frameworkElement.ParentOfType<FrameworkElement>())
            {
                if (frameworkElement is SplitButton splitButton)
                {
                    splitButton.SetCurrentValue(SplitButton.IsOpenProperty, false);
                }
                else if (frameworkElement is DropDownButton radDropDownButton)
                {
                    radDropDownButton.SetCurrentValue(IsOpenProperty, false);
                }
            }

            this.SetCurrentValue(IsOpenProperty, false);
        }

        private void InitializeDropdownPopup()
        {
            this.DropDownPopup?.RemoveHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnPopupMouseUp));

            var templateChild = this.GetTemplateChild(nameof(this.DropDownPopup));
            this.DropDownPopup = templateChild as Popup;
            if (this.DropDownPopup != null)
            {
                this.DropDownPopup.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnPopupMouseUp), true);
                this.DropDownPopup.SetCurrentValue(Popup.StaysOpenProperty, this.KeepOpen);
            }
        }

        private bool IsElementInPopup(DependencyObject element)
        {
            var popup = element?.ParentOfType<Popup>();
            if (popup != null && popup.Equals(this.DropDownPopup))
            {
                return true;
            }

            return false;
        }

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

        private static void OnAutoOpenDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        private static void OnDropDownContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        private static void OnDropDownContentTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        private static void OnDropDownContentTemplateSelectorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }
    }
}