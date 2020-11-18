using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    public class NullToVisibilityConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value != null;

            switch (value)
            {
                case ICollection collection:
                    b = collection.Count != 0;
                    break;

                case string str:
                    b = !string.IsNullOrWhiteSpace(str);
                    break;

                case int i:
                    b = i != 0;
                    break;

                case double d:
                    b = Math.Abs(d) > 0.01;
                    break;
            }

            if ((parameter is bool inverse || parameter is string s && bool.TryParse(s, out inverse)) && inverse)
            {
                b = !b;
            }

            if (targetType == typeof(Visibility))
            {
                return b ? Visibility.Visible : Visibility.Collapsed;
            }

            return b;
        }


        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}