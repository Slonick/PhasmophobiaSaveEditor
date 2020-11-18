namespace PhasmophobiaSaveEditor.Native
{
    internal static class NativeEnums
    {
        internal enum AccentFlagsType
        {
            Window = 0,
            Popup
        }
        
        internal enum WindowCompositionAttribute
        {
            WcaAccentPolicy = 19
        }

        internal enum AccentState
        {
            AccentDisabled = 0,
            AccentEnableGradient = 1,
            AccentEnableTransparentGradient = 2,
            AccentEnableBlurBehind = 3,
            AccentEnableAcrylicBlurBehind = 4,
            AccentInvalidState = 5
        }
        
        public enum MonitorOptions : uint
        {
            MonitorDefaultToNull = 0x00000000,
            MonitorDefaultToPrimary = 0x00000001,
            MonitorDefaultToNearest = 0x00000002
        }
    }
}