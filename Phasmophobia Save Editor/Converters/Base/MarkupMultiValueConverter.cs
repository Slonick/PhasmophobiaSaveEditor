using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace PhasmophobiaSaveEditor.Converters.Base
{
    /// <summary>
    ///     MarkupMultiValueConverter is a MarkupExtension which can be used for MultiValueConverter.
    /// </summary>
    /// <seealso cref="System.Windows.Markup.MarkupExtension"/>
    /// <seealso cref="System.Windows.Data.IMultiValueConverter"/>
    [MarkupExtensionReturnType(typeof(IMultiValueConverter))]
    public class MarkupMultiValueConverter : MarkupExtension, IMultiValueConverter
    {
        /// <inheritdoc/>
        public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        /// <inheritdoc/>
        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}