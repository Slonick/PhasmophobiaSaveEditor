using System;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    internal class CornerRadiusConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is CornerRadius))
            {
                throw new ArgumentException(@"Value should be CornerRadius.", nameof(value));
            }

            if (parameter == null)
            {
                return value;
            }

            var cornerRadius = (CornerRadius) value;
            var text = parameter.ToString().ToLowerInvariant();
            var left = text.Contains("left");
            var top = text.Contains("top");
            var right = text.Contains("right");
            var bottom = text.Contains("bottom");
            var inverse = text.Contains("inverse");
            return inverse
                ? new CornerRadius(top && left ? 0.0 : cornerRadius.TopLeft, top && right ? 0.0 : cornerRadius.TopRight, right && bottom ? 0.0 : cornerRadius.BottomRight, left && bottom ? 0.0 : cornerRadius.BottomLeft)
                : new CornerRadius(top && left ? cornerRadius.TopLeft : 0.0, top && right ? cornerRadius.TopRight : 0.0, right && bottom ? cornerRadius.BottomRight : 0.0, left && bottom ? cornerRadius.BottomLeft : 0.0);
        }
    }
}