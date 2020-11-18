using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    internal class BrushTranslationConverter : MarkupMultiValueConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(o => o == DependencyProperty.UnsetValue || o == null)) return new Point(0, 0);

            var parent = values[0] as UIElement;
            var ctrl = values[1] as UIElement;
            //var pointerPos = (Point)values[2];
            var relativePos = parent.TranslatePoint(new Point(0, 0), ctrl);

            return new TranslateTransform(relativePos.X, relativePos.Y);
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}