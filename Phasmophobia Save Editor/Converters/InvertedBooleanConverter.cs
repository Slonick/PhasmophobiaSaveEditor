using System;
using System.Globalization;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    internal class InvertedBooleanConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is bool valueAsBool ? !valueAsBool : value;

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is bool valueAsBool ? !valueAsBool : value;
    }
}