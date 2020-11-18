using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using PhasmophobiaSaveEditor.Native;

namespace PhasmophobiaSaveEditor.Utils
{
    /// <summary>
    ///     Fixes the issue with Windows of Style <see cref="WindowStyle.None"/> covering the taskbar
    /// </summary>
    public class WindowResizer
    {
        /// <summary>
        ///     The window to handle the resizing for
        /// </summary>
        private readonly Window window;

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="window">The window to monitor and correctly maximize</param>
        public WindowResizer(Window window)
        {
            this.window = window;

            // Listen out for source initialized to setup
            this.window.SourceInitialized += this.Window_SourceInitialized;
        }

        /// <summary>
        ///     Initialize and hook into the windows message pump
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            // Get the handle of this window
            var handle = new WindowInteropHelper(this.window).Handle;
            var handleSource = HwndSource.FromHwnd(handle);

            // Hook into it's Windows messages
            handleSource?.AddHook(this.WindowProc);

            this.window.SourceInitialized -= this.Window_SourceInitialized;
        }

        /// <summary>
        ///     Listens out for all windows messages for this window
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                // Handle the GetMinMaxInfo of the Window
                case 0x0024: // WM_GETMINMAXINFO
                    this.WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        ///     Get the min/max window size for this window
        ///     Correctly accounting for the task bar size and position
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            if (lParam == IntPtr.Zero) return;

            var lPrimaryScreen = NativeMethods.MonitorFromWindow(hwnd, NativeEnums.MonitorOptions.MonitorDefaultToNull);

            // Try and get the primary screen information
            var lPrimaryScreenInfo = new NativeStructs.MonitorInfo();
            if (NativeMethods.GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false) return;

            // Get min/max structure to fill with information
            var lMmi = (NativeStructs.MinMaxinfo) Marshal.PtrToStructure(lParam, typeof(NativeStructs.MinMaxinfo));

            lMmi.PointMaxPosition.X = Math.Abs(lPrimaryScreenInfo.RCWork.Left - lPrimaryScreenInfo.RCMonitor.Left);
            lMmi.PointMaxPosition.Y = Math.Abs(lPrimaryScreenInfo.RCWork.Top - lPrimaryScreenInfo.RCMonitor.Top);
            lMmi.PointMaxSize.X = Math.Abs(lPrimaryScreenInfo.RCWork.Right - lPrimaryScreenInfo.RCWork.Left);
            lMmi.PointMaxSize.Y = Math.Abs(lPrimaryScreenInfo.RCWork.Bottom - lPrimaryScreenInfo.RCWork.Top);

            var source = HwndSource.FromHwnd(hwnd);
            if (source?.CompositionTarget != null)
            {
                var scale = source.CompositionTarget.TransformToDevice.M11;

                lMmi.PointMinTrackSize.X = (int) (this.window.MinWidth * scale);
                lMmi.PointMinTrackSize.Y = (int) (this.window.MinHeight * scale);
            }

            // Now we have the max size, allow the host to tweak as needed
            Marshal.StructureToPtr(lMmi, lParam, true);
        }

        public static void ApplyToWindow(Window window) => _ = new WindowResizer(window);
    }
}