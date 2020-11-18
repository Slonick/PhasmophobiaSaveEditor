using System;
using System.Runtime.InteropServices;

namespace PhasmophobiaSaveEditor.Native
{
    internal static class NativeStructs
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public NativeEnums.WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public NativeEnums.AccentState AccentState;
            public int AccentFlags;
            public uint GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MonitorInfo
        {
            public int CBSize = Marshal.SizeOf(typeof(MonitorInfo));
            public Rectangle RCMonitor = new Rectangle();
            public Rectangle RCWork = new Rectangle();
            public int DWFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rectangle
        {
            public Rectangle(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            public int Left, Top, Right, Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MinMaxinfo
        {
            public Point PointReserved;
            public Point PointMaxSize;
            public Point PointMaxPosition;
            public Point PointMinTrackSize;
            public Point PointMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            /// <summary>
            ///     Construct a point of coordinates (x,y).
            /// </summary>
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override string ToString()
            {
                return $"{this.X} {this.Y}";
            }

            /// <summary>
            ///     x coordinate of point.
            /// </summary>
            public int X;

            /// <summary>
            ///     y coordinate of point.
            /// </summary>
            public int Y;
        }
    }
}