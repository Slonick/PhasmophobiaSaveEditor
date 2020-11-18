using System.Windows;

namespace PhasmophobiaSaveEditor.Controls.Dialog
{
    public class DialogParameters
    {
        public DialogButton Button { get; set; } = DialogButton.OK;

        /// <summary>
        ///     Gets or sets the content to be displayed.
        /// </summary>
        /// <value>The content to be displayed.</value>
        public object Content { get; set; }

        /// <summary>
        ///     Gets or sets a WindowStartupLocation value for the predefined dialogs.
        /// </summary>
        public WindowStartupLocation DialogStartupLocation { get; set; } = WindowStartupLocation.CenterOwner;

        /// <summary>
        ///     Gets or sets the object to appear in the title bar.
        /// </summary>
        /// <value>The object to appear in the title bar.</value>
        public object Header { get; set; }

        /// <summary>
        ///     Gets or sets the icon to be displayed.
        /// </summary>
        /// <value>The icon to be displayed.</value>
        public DialogIcon Icon { get; set; } = DialogIcon.None;

        /// <summary>
        ///     Gets or sets the Owner of the Dialog.
        /// </summary>
        public Window Owner { get; set; }

        public static DialogParameters Error(string text, string title = null)
            => new DialogParameters
            {
                Content = text,
                Header = title,
                Icon = DialogIcon.Error,
                DialogStartupLocation = WindowStartupLocation.CenterOwner
            };

        public static DialogParameters Success(string text, string title = null)
            => new DialogParameters
            {
                Content = text,
                Header = title,
                Icon = DialogIcon.Success,
                DialogStartupLocation = WindowStartupLocation.CenterOwner
            };

        public static DialogParameters Warning(string text, string title = null)
            => new DialogParameters
            {
                Content = text,
                Header = title,
                Icon = DialogIcon.Warning,
                DialogStartupLocation = WindowStartupLocation.CenterOwner
            };
    }
}