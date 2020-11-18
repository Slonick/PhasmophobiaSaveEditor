using System.Linq;
using System.Management;
using System.Windows;
using PhasmophobiaSaveEditor.Infrastructures;

namespace PhasmophobiaSaveEditor.Utils
{
    public static class WindowsOSHelper
    {
        private static WindowsVersion cachedWindowsVersion = WindowsVersion.Unsupported;

        public static void ActivateCurrentWindow()
        {
            var currentWindow = GetCurrentWindow();
            currentWindow?.Activate();
        }

        public static Window GetCurrentWindow()
        {
            if (Application.Current == null)
            {
                return null;
            }

            if (Application.Current.MainWindow?.IsActive ?? false)
            {
                return Application.Current.MainWindow;
            }

            return Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.IsActive);
        }

        public static string GetOSFriendlyName()
        {
            using (var enumerator = new ManagementObjectSearcher("SELECT Version FROM Win32_OperatingSystem").Get().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    var obj = ((ManagementObject) enumerator.Current)["Version"];
                }
            }

            return string.Empty;
        }

        public static WindowsVersion GetWindowsVersion()
        {
            if (cachedWindowsVersion != WindowsVersion.Unsupported)
            {
                return cachedWindowsVersion;
            }

            var windowsVersion = WindowsVersion.Unsupported;
            var text = GetWindowsVersionNumber();

            if (text.StartsWith("6.1"))
            {
                windowsVersion = WindowsVersion.Win7;
            }
            else if (text.StartsWith("6.2"))
            {
                windowsVersion = WindowsVersion.Win8;
            }
            else if (text.StartsWith("6.3"))
            {
                windowsVersion = WindowsVersion.Win81;
            }
            else if (text.StartsWith("10."))
            {
                var version = int.Parse(text.Split('.')[2]);
                switch (version)
                {
                    case 10240:
                        windowsVersion = WindowsVersion.Win10T1;
                        break;
                    case 10586:
                        windowsVersion = WindowsVersion.Win10T2;
                        break;
                    case 14393:
                        windowsVersion = WindowsVersion.Win10Rs1;
                        break;
                    case 15063:
                        windowsVersion = WindowsVersion.Win10Rs2;
                        break;
                    case 16299:
                        windowsVersion = WindowsVersion.Win10Rs3;
                        break;
                    case 17134:
                        windowsVersion = WindowsVersion.Win10Rs4;
                        break;
                    case 17763:
                        windowsVersion = WindowsVersion.Win10Rs5;
                        break;
                    case 18362:
                        windowsVersion = WindowsVersion.Win1019H1;
                        break;
                    case 18363:
                        windowsVersion = WindowsVersion.Win1019H2;
                        break;
                    default:
                        windowsVersion = WindowsVersion.Win10Beta;
                        break;
                }
            }

            cachedWindowsVersion = windowsVersion;

            return cachedWindowsVersion;
        }

        public static string GetWindowsVersionNumber()
        {
            var result = "";
            using (var managementObjectSearcher = new ManagementObjectSearcher("SELECT Version FROM Win32_OperatingSystem"))
            {
                using (var enumerator = managementObjectSearcher.Get().GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        result = enumerator.Current["Version"].ToString();
                    }
                }
            }

            return result;
        }
    }
}