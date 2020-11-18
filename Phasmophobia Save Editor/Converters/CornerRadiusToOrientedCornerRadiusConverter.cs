using System;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    public class CornerRadiusToOrientedCornerRadiusConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CornerRadius result = default;
            if (value is CornerRadius cornerRadius && parameter is string)
            {
                var text = parameter.ToString().ToLower(CultureInfo.InvariantCulture);
                result.TopLeft = text.Contains("tleft") ? cornerRadius.TopLeft : 0.0;
                result.TopRight = text.Contains("tright") ? cornerRadius.TopRight : 0.0;
                result.BottomLeft = text.Contains("bleft") ? cornerRadius.BottomLeft : 0.0;
                result.BottomRight = text.Contains("bright") ? cornerRadius.BottomRight : 0.0;
            }

            return result;
        }
    }
}