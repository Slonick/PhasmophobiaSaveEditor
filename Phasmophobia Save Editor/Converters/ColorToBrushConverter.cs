using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    public class ColorToBrushConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                if (parameter is null)
                {
                    return new SolidColorBrush(color);
                }

                var a = System.Convert.ToByte(parameter);
                return new SolidColorBrush(Color.FromArgb(a, color.R, color.G, color.B));
            }

            return DependencyProperty.UnsetValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush solidColorBrush)
            {
                return solidColorBrush.Color;
            }

            return null;
        }
    }
}