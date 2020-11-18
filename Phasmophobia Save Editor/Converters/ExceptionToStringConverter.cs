using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    [ValueConversion(typeof(Exception), typeof(string))]
    public class ExceptionToStringConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Exception exception))
            {
                return DependencyProperty.UnsetValue;
            }

            var builder = new StringBuilder();
            builder.AppendLine(exception.Message);
            builder.AppendLine(exception.StackTrace);
            return builder.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}