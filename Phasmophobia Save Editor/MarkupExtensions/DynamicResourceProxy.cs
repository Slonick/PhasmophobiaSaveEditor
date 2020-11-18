using System.Windows;

namespace PhasmophobiaSaveEditor.MarkupExtensions
{
    /// <summary>
    ///     Provides a mechanism to proxy dynamic resources in order to be converted or manipulated in xaml.
    /// </summary>
    public class DynamicResourceProxy : Freezable
    {
        /// <summary>
        ///     Identifies the <see cref="P:PhasmophobiaSaveEditor.MarkupExtensions.DynamicResourceProxy.ProxyValue" />
        ///     dependency property.
        /// </summary>
        public static readonly DependencyProperty ProxyValueProperty = DependencyProperty.Register("ProxyValue", typeof(object), typeof(DynamicResourceProxy), new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the value to be exposed by the proxy.
        /// </summary>
        public object ProxyValue
        {
            get => this.GetValue(ProxyValueProperty);
            set => this.SetValue(ProxyValueProperty, value);
        }

        /// <inheritdoc />
        protected override Freezable CreateInstanceCore() => new DynamicResourceProxy();

        /// <summary>
        ///     Makes the <see cref="T:System.Windows.Freezable" /> object unmodifiable or tests whether it can be made
        ///     unmodifiable.
        /// </summary>
        /// <param name="isChecking">
        ///     True to return an indication of whether the object can be frozen (without actually freezing
        ///     it); false to actually freeze the object.
        /// </param>
        /// <returns>
        ///     If <paramref name="isChecking" /> is true, this method returns true if the
        ///     <see cref="T:System.Windows.Freezable" /> can be made unmodifiable, or false if it cannot be made unmodifiable. If
        ///     <paramref name="isChecking" /> is false, this method returns true if the if the specified
        ///     <see cref="T:System.Windows.Freezable" /> is now unmodifiable, or false if it cannot be made unmodifiable.
        /// </returns>
        protected override bool FreezeCore(bool isChecking) => true;
    }
}