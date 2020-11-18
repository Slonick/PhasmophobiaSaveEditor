using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace PhasmophobiaSaveEditor.Themes
{
    /// <summary>
	/// Keys for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" /> resources.
	/// </summary>
	[TypeConverter(typeof(FluentResourceKeyTypeConverter))]
	public sealed class FluentResourceKey : ResourceKey
	{
		internal FluentResourceKey(FluentResourceKeyID key)
		{
			this.Key = key;
		}

		internal FluentResourceKeyID Key { get; private set; }

		/// <inheritdoc />
		public override Assembly Assembly
		{
			get => typeof(FluentResourceKey).Assembly;
        }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Key.ToString();
		}

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s AccentBrush.
		/// </summary>
		public static readonly ResourceKey ScrollBarMode = new FluentResourceKey(FluentResourceKeyID.ScrollBarMode);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s AccentBrush.
		/// </summary>
		public static readonly ResourceKey AccentBrush = new FluentResourceKey(FluentResourceKeyID.AccentBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s AccentMouseOverBrush.
		/// </summary>
		public static readonly ResourceKey AccentMouseOverBrush = new FluentResourceKey(FluentResourceKeyID.AccentMouseOverBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s AccentPressedBrush.
		/// </summary>
		public static readonly ResourceKey AccentPressedBrush = new FluentResourceKey(FluentResourceKeyID.AccentPressedBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s AccentFocusedBrush.
		/// </summary>
		public static readonly ResourceKey AccentFocusedBrush = new FluentResourceKey(FluentResourceKeyID.AccentFocusedBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s BasicBrush.
		/// </summary>
		public static readonly ResourceKey BasicBrush = new FluentResourceKey(FluentResourceKeyID.BasicBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s BasicSolidBrush.
		/// </summary>
		public static readonly ResourceKey BasicSolidBrush = new FluentResourceKey(FluentResourceKeyID.BasicSolidBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s IconBrush.
		/// </summary>
		public static readonly ResourceKey IconBrush = new FluentResourceKey(FluentResourceKeyID.IconBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s MainBrush.
		/// </summary>
		public static readonly ResourceKey MainBrush = new FluentResourceKey(FluentResourceKeyID.MainBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s MarkerBrush.
		/// </summary>
		public static readonly ResourceKey MarkerBrush = new FluentResourceKey(FluentResourceKeyID.MarkerBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s MarkerMouseOverBrush.
		/// </summary>
		public static readonly ResourceKey MarkerMouseOverBrush = new FluentResourceKey(FluentResourceKeyID.MarkerMouseOverBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s ValidationBrush.
		/// </summary>
		public static readonly ResourceKey ValidationBrush = new FluentResourceKey(FluentResourceKeyID.ValidationBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s ComplementaryBrush.
		/// </summary>
		public static readonly ResourceKey ComplementaryBrush = new FluentResourceKey(FluentResourceKeyID.ComplementaryBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s MouseOverBrush.
		/// </summary>
		public static readonly ResourceKey MouseOverBrush = new FluentResourceKey(FluentResourceKeyID.MouseOverBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s PressedBrush.
		/// </summary>
		public static readonly ResourceKey PressedBrush = new FluentResourceKey(FluentResourceKeyID.PressedBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s AlternativeBrush.
		/// </summary>
		public static readonly ResourceKey AlternativeBrush = new FluentResourceKey(FluentResourceKeyID.AlternativeBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s MarkerInvertedBrush.
		/// </summary>
		public static readonly ResourceKey MarkerInvertedBrush = new FluentResourceKey(FluentResourceKeyID.MarkerInvertedBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s PrimaryBrush.
		/// </summary>
		public static readonly ResourceKey PrimaryBrush = new FluentResourceKey(FluentResourceKeyID.PrimaryBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s PrimaryBackgroundBrush.
		/// </summary>
		public static readonly ResourceKey PrimaryBackgroundBrush = new FluentResourceKey(FluentResourceKeyID.PrimaryBackgroundBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s PrimaryMouseOverBrush.
		/// </summary>
		public static readonly ResourceKey PrimaryMouseOverBrush = new FluentResourceKey(FluentResourceKeyID.PrimaryMouseOverBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s ReadOnlyBackgroundBrush.
		/// </summary>
		public static readonly ResourceKey ReadOnlyBackgroundBrush = new FluentResourceKey(FluentResourceKeyID.ReadOnlyBackgroundBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s ReadOnlyBorderBrush.
		/// </summary>
		public static readonly ResourceKey ReadOnlyBorderBrush = new FluentResourceKey(FluentResourceKeyID.ReadOnlyBorderBrush);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s FontSizeS.
		/// </summary>
		public static readonly ResourceKey FontSizeS = new FluentResourceKey(FluentResourceKeyID.FontSizeS);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s FontSize.
		/// </summary>
		public static readonly ResourceKey FontSize = new FluentResourceKey(FluentResourceKeyID.FontSize);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s FontSizeL.
		/// </summary>
		public static readonly ResourceKey FontSizeL = new FluentResourceKey(FluentResourceKeyID.FontSizeL);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s FontSizeXL.
		/// </summary>
		public static readonly ResourceKey FontSizeXL = new FluentResourceKey(FluentResourceKeyID.FontSizeXL);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s FontFamily.
		/// </summary>
		public static readonly ResourceKey FontFamily = new FluentResourceKey(FluentResourceKeyID.FontFamily);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s CornerRadius.
		/// </summary>
		public static readonly ResourceKey CornerRadius = new FluentResourceKey(FluentResourceKeyID.CornerRadius);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s CornerRadiusTop.
		/// </summary>
		public static readonly ResourceKey CornerRadiusTop = new FluentResourceKey(FluentResourceKeyID.CornerRadiusTop);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s CornerRadiusBottom.
		/// </summary>
		public static readonly ResourceKey CornerRadiusBottom = new FluentResourceKey(FluentResourceKeyID.CornerRadiusBottom);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s CornerRadiusLeft.
		/// </summary>
		public static readonly ResourceKey CornerRadiusLeft = new FluentResourceKey(FluentResourceKeyID.CornerRadiusLeft);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s CornerRadiusRight.
		/// </summary>
		public static readonly ResourceKey CornerRadiusRight = new FluentResourceKey(FluentResourceKeyID.CornerRadiusRight);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s DisabledOpacity.
		/// </summary>
		public static readonly ResourceKey DisabledOpacity = new FluentResourceKey(FluentResourceKeyID.DisabledOpacity);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s InputOpacity.
		/// </summary>
		public static readonly ResourceKey InputOpacity = new FluentResourceKey(FluentResourceKeyID.InputOpacity);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s ReadOnlyOpacity.
		/// </summary>
		public static readonly ResourceKey ReadOnlyOpacity = new FluentResourceKey(FluentResourceKeyID.ReadOnlyOpacity);

		/// <summary>
		/// The key for the <see cref="T:PhasmophobiaSaveEditor.FluentPalette" />'s FocusThickness.
		/// </summary>
		public static readonly ResourceKey FocusThickness = new FluentResourceKey(FluentResourceKeyID.FocusThickness);
	}
}
