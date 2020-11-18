using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;
using PhasmophobiaSaveEditor.Controls;
using PhasmophobiaSaveEditor.Infrastructures;
using PhasmophobiaSaveEditor.Native;

namespace PhasmophobiaSaveEditor.Utils
{
    internal static class AcrylicHelper
    {
        internal static void EnableBlur(FluentWindow window, uint opacity = 0, NativeEnums.AccentFlagsType style = NativeEnums.AccentFlagsType.Window)
        {
            var brush = window.FluentBackground;

            try
            {
                var handle = new WindowInteropHelper(window).Handle;
                var version = WindowsOSHelper.GetWindowsVersion();
                if (version >= WindowsVersion.Win7 && version < WindowsVersion.Win10T1)
                {
                    window.Background = new SolidColorBrush(Color.FromArgb((byte) opacity, brush.Color.R, brush.Color.G, brush.Color.B));
                }
                else if (version >= WindowsVersion.Win10T1)
                {
                    var accent = new NativeStructs.AccentPolicy
                    {
                        AccentState = window.FluentIsEnabled ? NativeEnums.AccentState.AccentEnableBlurBehind : NativeEnums.AccentState.AccentDisabled,
                        AccentFlags = style == NativeEnums.AccentFlagsType.Window ? 2 : 480,
                        GradientColor = BitConverter.ToUInt32(new byte[] {brush.Color.R, brush.Color.G, brush.Color.B, 0}, 0)
                    };

                    var accentStructSize = Marshal.SizeOf(accent);
                    var accentPtr = Marshal.AllocHGlobal(accentStructSize);
                    Marshal.StructureToPtr(accent, accentPtr, false);

                    var data = new NativeStructs.WindowCompositionAttributeData
                    {
                        Attribute = NativeEnums.WindowCompositionAttribute.WcaAccentPolicy,
                        SizeOfData = accentStructSize,
                        Data = accentPtr
                    };

                    var result = NativeMethods.SetWindowCompositionAttribute(handle, ref data);
                    Marshal.FreeHGlobal(accentPtr);
                    window.Background = new SolidColorBrush(Color.FromArgb((byte) (window.FluentIsEnabled ? opacity : 255), brush.Color.R, brush.Color.G, brush.Color.B));
                }
            }
            catch (DllNotFoundException)
            {
                window.Background = new SolidColorBrush(Color.FromArgb((byte) opacity, brush.Color.R, brush.Color.G, brush.Color.B));
            }
        }
    }
}