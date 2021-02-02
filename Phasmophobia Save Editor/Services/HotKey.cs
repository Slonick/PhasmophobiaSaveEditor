using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace PhasmophobiaSaveEditor.Services
{
    public static class HotKey
    {
        public const int WmHotKey = 0x0312;

        private static Dictionary<int, ICommand> dictHotKeyToCalBackProc;

        public static void Register(Key key, KeyModifier keyModifiers, ICommand command)
        {
            var virtualKeyCode = KeyInterop.VirtualKeyFromKey(key);
            var id = virtualKeyCode + (int) keyModifiers * 0x10000;
            RegisterHotKey(IntPtr.Zero, id, (uint) keyModifiers, (uint) virtualKeyCode);

            if (dictHotKeyToCalBackProc == null)
            {
                dictHotKeyToCalBackProc = new Dictionary<int, ICommand>();
                ComponentDispatcher.ThreadFilterMessage += ComponentDispatcherThreadFilterMessage;
            }

            dictHotKeyToCalBackProc.Add(id, command);
        }

        private static void ComponentDispatcherThreadFilterMessage(ref MSG msg, ref bool handled)
        {
            if (handled ||
                msg.message != WmHotKey ||
                !dictHotKeyToCalBackProc.TryGetValue((int) msg.wParam, out var command))
                return;

            if (command.CanExecute(null)) command.Execute(null);

            handled = true;
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vlc);
    }

    [Flags]
    public enum KeyModifier
    {
        None = 0x0000,
        Alt = 0x0001,
        Ctrl = 0x0002,
        NoRepeat = 0x4000,
        Shift = 0x0004,
        Win = 0x0008
    }
}