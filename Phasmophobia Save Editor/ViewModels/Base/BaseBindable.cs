using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace PhasmophobiaSaveEditor.ViewModels.Base
{
    [DataContract]
    public abstract class BaseBindable : INotifyPropertyChanged
    {
        /// <summary>
        ///     Occurs after a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Extracts the name of a property from an expression.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression returning the property's name.</param>
        /// <returns>The name of the property returned by the expression.</returns>
        /// <exception cref="T:System.ArgumentNullException">If the expression is null.</exception>
        /// <exception cref="T:System.ArgumentException">If the expression does not represent a property.</exception>
        protected string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            if (!(propertyExpression.Body is MemberExpression body))
            {
                throw new ArgumentException(@"Invalid argument", nameof(propertyExpression));
            }

            var member = body.Member as PropertyInfo;
            if (member == null)
            {
                throw new ArgumentException(@"Argument is not a property", nameof(propertyExpression));
            }

            return member.Name;
        }

        /// <summary>
        ///     Raises the PropertyChanged event if needed.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property that
        ///     changed.
        /// </param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Raises the PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyExpression">
        ///     An expression identifying the property
        ///     that changed.
        /// </param>
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler == null)
            {
                return;
            }

            var propertyName = this.GetPropertyName(propertyExpression);
            eventHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="propertyExpression">
        ///     An expression identifying the property
        ///     that changed.
        /// </param>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <returns>
        ///     True if the PropertyChanged event has been raised,
        ///     false otherwise. The event is not raised if the old
        ///     value is equal to the new value.
        /// </returns>
        protected virtual bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            this.RaisePropertyChanged(propertyExpression);
            return true;
        }

        /// <summary>
        ///     Assigns a new value to the property. Then, raises the
        ///     PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the property that
        ///     changed.
        /// </typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">
        ///     The property's value after the change
        ///     occurred.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property that
        ///     changed.
        /// </param>
        /// <returns>
        ///     True if the PropertyChanged event has been raised,
        ///     false otherwise. The event is not raised if the old
        ///     value is equal to the new value.
        /// </returns>
        protected virtual bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            field = newValue;
            this.RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        ///     Warns the developer if this object does not have
        ///     a public property with the specified name. This
        ///     method does not exist in a Release build.
        /// </summary>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (this.GetType().GetProperty(propertyName ?? throw new ArgumentNullException(nameof(propertyName))) == null)
            {
                throw new InvalidOperationException($"Property {nameof(propertyName)} not found");
            }
        }
    }
}