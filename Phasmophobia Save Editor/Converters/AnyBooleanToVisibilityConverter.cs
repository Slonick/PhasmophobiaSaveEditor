using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    public class AnyBooleanToVisibilityConverter : MarkupMultiValueConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is null)
            {
                return values.OfType<bool>().Any(v => v) ? Visibility.Visible : Visibility.Collapsed;
            }

            return values.OfType<bool>().Any(v => v) ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}