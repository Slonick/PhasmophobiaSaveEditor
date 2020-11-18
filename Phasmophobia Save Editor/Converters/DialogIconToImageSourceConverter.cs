using System;
using System.Globalization;
using System.Windows;
using PhasmophobiaSaveEditor.Controls.Dialog;
using PhasmophobiaSaveEditor.Converters.Base;

namespace PhasmophobiaSaveEditor.Converters
{
    public class DialogIconToImageSourceConverter : MarkupValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DialogIcon dialogIcon))
            {
                return DependencyProperty.UnsetValue;
            }

            switch (dialogIcon)
            {
                case DialogIcon.None:
                    return null;
                case DialogIcon.Error:
                    return Application.Current.Resources["ErrorDialogIcon"];
                case DialogIcon.Warning:
                    return Application.Current.Resources["WarningDialogIcon"];
                case DialogIcon.Question:
                    return Application.Current.Resources["QuestionDialogIcon"];
                case DialogIcon.Info:
                    return Application.Current.Resources["InfoDialogIcon"];
                case DialogIcon.Success:
                    return Application.Current.Resources["SuccessDialogIcon"];
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}