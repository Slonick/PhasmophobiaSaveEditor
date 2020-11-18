using System;
using System.Runtime.Serialization;
using System.Windows;
using PhasmophobiaSaveEditor.Infrastructures;
using PhasmophobiaSaveEditor.ViewModels;

namespace PhasmophobiaSaveEditor.Models.Configuration
{
    [DataContract]
    public class AppearanceOptions
    {
        /// <summary>
        ///     The height of the window.
        /// </summary>
        [DataMember]
        public double Height { get; private set; } = 450;

        [DataMember]
        public Language Language { get; set; } = Language.English;

        /// <summary>
        ///     The left coordinate of the window's position.
        /// </summary>
        [DataMember]
        public double Left { get; private set; } = double.NaN;

        /// <summary>
        ///     The state of the window (i.e. normal, minimized, maximized, etc...).
        /// </summary>
        [DataMember]
        public WindowState State { get; private set; } = WindowState.Normal;

        [DataMember]
        public Theme Theme { get; set; } = Theme.Dark;

        /// <summary>
        ///     The top coordinate of the window's position.
        /// </summary>
        [DataMember]
        public double Top { get; private set; } = double.NaN;

        /// <summary>
        ///     The width of the window.
        /// </summary>
        [DataMember]
        public double Width { get; private set; } = 400;

        /// <summary>
        ///     Restores the given window's values to the ones in this object.
        /// </summary>
        /// <param name="window"></param>
        public void RestoreTo(Window window)
        {
            window.SetCurrentValue(Window.LeftProperty, Math.Max(SystemParameters.VirtualScreenLeft, this.Left));
            window.SetCurrentValue(Window.TopProperty, Math.Max(SystemParameters.VirtualScreenTop, this.Top));
            window.SetCurrentValue(FrameworkElement.WidthProperty, this.Width);
            window.SetCurrentValue(FrameworkElement.HeightProperty, this.Height);
            window.SetCurrentValue(Window.WindowStateProperty, this.State);

            if (window.DataContext is MainViewModel vm)
            {
                vm.Language = this.Language;
                vm.Theme = this.Theme;
            }
        }

        /// <summary>
        ///     Fetches all values from the given window (to be saved to an xml file, for example).
        /// </summary>
        /// <param name="window"></param>
        public void UpdateFrom(Window window)
        {
            this.Left = window.Left;
            this.Top = window.Top;
            this.Width = window.Width;
            this.Height = window.Height;
            this.State = window.WindowState;

            if (window.DataContext is MainViewModel vm)
            {
                this.Language = vm.Language;
                this.Theme = vm.Theme;
            }
        }
    }
}