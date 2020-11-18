using System;
using System.Runtime.InteropServices;

namespace PhasmophobiaSaveEditor.Native
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr handle, ref NativeStructs.WindowCompositionAttributeData data);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, NativeEnums.MonitorOptions dwFlags);

        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, NativeStructs.MonitorInfo lpmi);
    }
}