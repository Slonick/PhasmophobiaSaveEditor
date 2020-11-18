using System;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    public class ThicknessToOrientedThicknessConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness result = default;
            if (value is Thickness thickness && parameter is string)
            {
                var text = parameter.ToString().ToLower(CultureInfo.InvariantCulture);
                result.Left = text.Contains("left") ? thickness.Left : 0.0;
                result.Top = text.Contains("top") ? thickness.Top : 0.0;
                result.Right = text.Contains("right") ? thickness.Right : 0.0;
                result.Bottom = text.Contains("bottom") ? thickness.Bottom : 0.0;
            }

            return result;
        }
    }
}