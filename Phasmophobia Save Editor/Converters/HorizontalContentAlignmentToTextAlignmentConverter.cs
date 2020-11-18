using System;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    internal class HorizontalContentAlignmentToTextAlignmentConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return TextAlignment.Left;
            }

            switch ((HorizontalAlignment) Enum.Parse(typeof(HorizontalAlignment), value.ToString(), true))
            {
                case HorizontalAlignment.Left:
                    return TextAlignment.Left;
                case HorizontalAlignment.Center:
                    return TextAlignment.Center;
                case HorizontalAlignment.Right:
                    return TextAlignment.Right;
                case HorizontalAlignment.Stretch:
                    return TextAlignment.Left;
                default:
                    return TextAlignment.Left;
            }
        }
    }
}