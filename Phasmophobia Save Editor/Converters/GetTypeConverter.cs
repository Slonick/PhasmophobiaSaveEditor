using System;
using System.Globalization;
using System.Windows.Data;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    [ValueConversion(typeof(object), typeof(Type))]
    public class GetTypeConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value?.GetType();
    }
}