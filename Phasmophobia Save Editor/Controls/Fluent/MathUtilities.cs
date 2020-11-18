using System;
using System.Linq;
using System.Windows;

namespace PhasmophobiaSaveEditor.Controls.Fluent
{
    /// <summary>
    ///     Provides static methods not included in the standard Math class.
    /// </summary>
    public static class MathUtilities
    {
        internal const double DoubleEpsilon = 2.2204460492503131E-16;

        internal static double PointDifferenceThreshold { get; set; } = 5.0;

        /// <summary>
        ///     Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            var num = value > max ? max : value;

            if (num < min)
            {
                num = min;
            }

            return num;
        }

        /// <summary>
        ///     Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static double Clamp(double value, double min, double max)
        {
            var num = value > max ? max : value;

            if (num < min)
            {
                num = min;
            }

            return num;
        }

        /// <summary>
        ///     Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="precision">The rounding precision value.</param>
        /// <returns>The clamped value.</returns>
        public static double Clamp(double value, double minimum, double maximum, int precision)
        {
            var value2 = Clamp(value, minimum, maximum);
            return Math.Round(value2, precision);
        }

        /// <summary>
        ///     Checks if a value is within a specified range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>True if the values is within the range, false otherwise.</returns>
        public static bool IsInRange(int value, int min, int max) => value >= min && value <= max;

        internal static bool AreAllPositive(params int[] values) => values.All(num => num >= 0);

        internal static bool AreClose(Point point1, Point point2)
        {
            var vector = new Vector(point1.X - point2.X, point1.Y - point2.Y);
            var num = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return num < PointDifferenceThreshold;
        }

        /// <summary>
        ///     Checks if a value is negligibly small and close to 0.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the values is negligibly small, false otherwise.</returns>
        internal static bool IsCloseToZero(double value) => Math.Abs(value) < 2.2204460492503131E-15;

        internal static Point ToCartesianCoordinates(double radius, double angleDeg)
        {
            var x = radius * Math.Cos(angleDeg / 180.0 * 3.1415926535897931);
            var y = radius * Math.Sin(angleDeg / 180.0 * 3.1415926535897931);
            return new Point(x, y);
        }

        /// <summary>
        ///     Converts cartesian into polar coordinates.
        /// </summary>
        /// <param name="point">The point we are converting.</param>
        /// <param name="centerPoint">The (0,0) point of the the coordinate system.</param>
        /// <param name="reverse">True to reverse the calculated angle using the (360 - angle) expression, false otherwise.</param>
        /// <returns> Coordinates as radius and angle (in degrees).</returns>
        internal static Tuple<double, double> ToPolarCoordinates(Point point, Point centerPoint, bool reverse = false)
        {
            var num = point.X - centerPoint.X;
            var num2 = Math.Abs(point.Y - centerPoint.Y);
            var num3 = Math.Sqrt(num * num + num2 * num2);
            var num4 = Math.Asin(num2 / num3) * 180.0 / 3.1415926535897931;
            if (centerPoint.X < point.X && centerPoint.Y > point.Y)
            {
                num4 = 360.0 - num4;
            }
            else if (centerPoint.X >= point.X && centerPoint.Y > point.Y)
            {
                num4 += 180.0;
            }
            else if (centerPoint.X >= point.X && centerPoint.Y <= point.Y)
            {
                num4 = 180.0 - num4;
            }

            if (reverse)
            {
                num4 = 360.0 - num4;
            }

            return new Tuple<double, double>(num3, num4);
        }
    }
}