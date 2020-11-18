using System;
using System.Windows;

namespace PhasmophobiaSaveEditor.AttachedProperties
{
    /// <summary>
    ///     A base attached property to replace the vanilla WPF attached property
    /// </summary>
    /// <typeparam name="TParent">The parent class to be the attached property</typeparam>
    /// <typeparam name="TProperty">The type of this attached property</typeparam>
    public abstract class BaseAttachedProperty<TParent, TProperty>
        where TParent : new()
    {
        /// <summary>
        ///     Fired when the value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

        /// <summary>
        ///     Fired when the value changes, even when the value is the same
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        /// <summary>
        ///     The attached property for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(TProperty), typeof(BaseAttachedProperty<TParent, TProperty>), new UIPropertyMetadata(default(TProperty), OnValueChanged, CoerceValue));

        /// <summary>
        ///     A singleton instance of our parent class
        /// </summary>
        public static TParent Instance { get; } = new TParent();

        /// <summary>
        ///     The method that is called when any attached property of this type is changed
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="e">The arguments for this event</param>
        public virtual void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        ///     The method that is called when any attached property of this type is changed, even if the value is the same
        /// </summary>
        /// <param name="sender">The UI element that this property was changed for</param>
        /// <param name="value">The arguments for this event</param>
        public virtual void OnValuePropertyUpdated(DependencyObject sender, object value) { }

        /// <summary>Helper for getting <see cref="ValueProperty" /> from <paramref name="d" />.</summary>
        /// <param name="d"><see cref="DependencyObject" /> to read <see cref="ValueProperty" /> from.</param>
        /// <returns>Value property value.</returns>
        [AttachedPropertyBrowsableForType(typeof(DependencyObject))]
        public static TProperty GetValue(DependencyObject d) => (TProperty) d.GetValue(ValueProperty);

        /// <summary>Helper for setting <see cref="ValueProperty" /> on <paramref name="d" />.</summary>
        /// <param name="d"><see cref="DependencyObject" /> to set <see cref="ValueProperty" /> on.</param>
        /// <param name="value">Value property value.</param>
        public static void SetValue(DependencyObject d, TProperty value) => d.SetValue(ValueProperty, value);

        /// <summary>
        ///     The callback event when the <see cref="ValueProperty" /> is changed, even if it is the same value
        /// </summary>
        /// <param name="d">The UI element that had it's property changed</param>
        /// <param name="value">The arguments for the event</param>
        private static object CoerceValue(DependencyObject d, object value)
        {
            // Call the parent function
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.OnValuePropertyUpdated(d, value);

            // Call event listeners
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.ValueUpdated(d, value);

            // Return the value
            return value;
        }

        /// <summary>
        ///     The callback event when the <see cref="ValueProperty" /> is changed
        /// </summary>
        /// <param name="d">The UI element that had it's property changed</param>
        /// <param name="e">The arguments for the event</param>
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Call the parent function
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.OnValuePropertyChanged(d, e);

            // Call event listeners
            (Instance as BaseAttachedProperty<TParent, TProperty>)?.ValueChanged(d, e);
        }
    }
}