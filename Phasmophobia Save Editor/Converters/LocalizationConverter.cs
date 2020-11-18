using System;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Converters.Base;
using PhasmophobiaSaveEditor.Infrastructures;

namespace PhasmophobiaSaveEditor.Converters
{
    public class LocalizationConverter : MarkupMultiValueConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is string key && values[1] is Language language)
            {
                if (Application.Current.Resources.Contains(key))
                {
                    return Application.Current.Resources[key];
                }

                return key;
            }

            return DependencyProperty.UnsetValue;
        }

        public override object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException($"{nameof(LocalizationConverter)} not implemented {nameof(this.ConvertBack)}");
    }
}