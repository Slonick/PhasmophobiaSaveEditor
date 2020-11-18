using System;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    public class EnumToBooleanConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            return Enum.IsDefined(value.GetType(), parameter) == false
                ? DependencyProperty.UnsetValue
                : parameter.Equals(value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && Enum.IsDefined(targetType, parameter))
            {
                return parameter;
            }

            return DependencyProperty.UnsetValue;
        }
    }
}