using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    /// <summary>
    ///     Adds opacity to a specified <seealso cref="T:System.Windows.Media.Color" /> or
    ///     <see cref="T:System.Windows.Media.SolidColorBrush" />.
    /// </summary>
    [ValueConversion(typeof(Color), typeof(Color))]
    [ValueConversion(typeof(SolidColorBrush), typeof(SolidColorBrush))]
    public class OpacityConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case Color color when parameter != null:
                {
                    var a = System.Convert.ToByte(parameter);
                    color.A = a;
                    return color;
                }
                case SolidColorBrush solidColorBrush when parameter != null:
                {
                    if (solidColorBrush.CanFreeze)
                    {
                        solidColorBrush.Freeze();
                    }

                    var a = System.Convert.ToByte(parameter);
                    return new SolidColorBrush(Color.FromArgb(a, solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B));
                }
                default:
                    return value;
            }
        }
    }
}