using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace PhasmophobiaSaveEditor.Themes
{
    /// <summary>
    ///     Used to convert <see cref="T:System.Windows.ResourceKey"/> types used to consume
    ///     <see cref="T:PhasmophobiaSaveEditor.ThemePalette"/>s.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="T:System.Windows.ResourceKey"/> used with this converter.</typeparam>
    public abstract class ThemeResourceKeyTypeConverter<T> : TypeConverter where T : ResourceKey
    {
        internal ThemeResourceKeyTypeConverter() { }

        internal abstract IDictionary<string, ResourceKey> Keys { get; }

        /// <inheritdoc/>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        /// <inheritdoc/>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(MarkupExtension) && context is IValueSerializerContext || base.CanConvertTo(context, destinationType);

        /// <inheritdoc/>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string text))
            {
                return base.ConvertFrom(context, culture, value);
            }

            if (this.Keys.TryGetValue(text, out var result))
            {
                return result;
            }

            if (!text.StartsWith("{x:Static themes:") || !text.EndsWith("}"))
            {
                return base.ConvertFrom(context, culture, value);
            }

            var array = text.Substring(18, text.Length - 19).Split('.');
            if (array.Length == 2 && this.Keys.TryGetValue(array[1], out result))
            {
                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <inheritdoc/>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return string.Concat("{x:Static themes:", typeof(T).Name, ".", Convert.ToString(value), "}");
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}