using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PhasmophobiaSaveEditor.Themes
{
    /// <summary>
    ///     Holds infrastructure for the bound resource in different themes.
    /// </summary>
    public abstract class ThemePalette : Freezable
    {
        internal ThemePalette() { }

        /// <summary>
        ///     When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" />
        ///     derived class.
        /// </summary>
        /// <returns>
        ///     The new instance.
        /// </returns>
        protected sealed override Freezable CreateInstanceCore() => throw new NotImplementedException();

        /// <summary>
        ///     Makes the <see cref="T:System.Windows.Freezable" /> object unmodifiable or tests whether it can be made
        ///     unmodifiable.
        /// </summary>
        /// <param name="isChecking">
        ///     True to return an indication of whether the object can be frozen (without actually freezing
        ///     it); false to actually freeze the object.
        /// </param>
        /// <returns>
        ///     If <paramref name="isChecking" /> is true, this method returns true if the
        ///     <see cref="T:System.Windows.Freezable" /> can be made unmodifiable, or false if it cannot be made unmodifiable. If
        ///     <paramref name="isChecking" /> is false, this method returns true if the if the specified
        ///     <see cref="T:System.Windows.Freezable" /> is now unmodifiable, or false if it cannot be made unmodifiable.
        /// </returns>
        protected sealed override bool FreezeCore(bool isChecking)
        {
            if (!isChecking)
            {
                this.FreezeOverride();
            }

            return base.FreezeCore(isChecking);
        }

        internal abstract void FreezeOverride();

        internal virtual void OnResourcePropertyChanged(DependencyPropertyChangedEventArgs e) { }

        internal static Color ColorFromLongValue(long colorValue)
        {
            var a = (byte) (colorValue >> 24);
            var r = (byte) ((colorValue & 16711680L) >> 16);
            var g = (byte) ((colorValue & 65280L) >> 8);
            var b = (byte) (colorValue & 255L);
            return Color.FromArgb(a, r, g, b);
        }

        internal static void FreezeAndSetResource(ResourceDictionary resourceDictionary, object key, object resource)
        {
            if (resource is Freezable freezable)
            {
                freezable.Freeze();
            }

            SetResource(resourceDictionary, key, resource);
        }

        internal static void OnIsFreezableChanged(DependencyObject d, DependencyPropertyChangedEventArgs args, DependencyProperty isFrozenProperty, DependencyObject freezeSource)
        {
            if (!(bool) args.NewValue)
            {
                throw new InvalidOperationException("ThemeResources.IsFreezable attached property can not be unset");
            }

            if (d is Freezable freezable)
            {
                var flag = (bool) freezeSource.GetValue(isFrozenProperty);
                if (flag)
                {
                    TryFreeze(freezable);
                    return;
                }

                if (freezable.ReadLocalValue(isFrozenProperty) == DependencyProperty.UnsetValue)
                {
                    BindingOperations.SetBinding(freezable, isFrozenProperty, new Binding
                    {
                        Path = new PropertyPath(isFrozenProperty),
                        Source = freezeSource
                    });
                }
            }
        }

        internal static void OnIsFrozenChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var flag = (bool) args.NewValue;
            if (flag)
            {
                if (d is Freezable freezable)
                {
                    TryFreeze(freezable);
                }
            }
        }

        internal static void OnResourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ThemePalette themePalette)
            {
                themePalette.OnResourcePropertyChanged(e);
            }
        }

        /// <summary>
        ///     Registers a Color dependency property.
        /// </summary>
        /// <typeparam name="TPalette">The value type of the <see cref="T:System.Windows.DependencyProperty" />.</typeparam>
        /// <param name="propertyName">The <see cref="T:System.Windows.DependencyProperty" /> name.</param>
        /// <param name="color">
        ///     The color represented as UInt32 in ARGB form, 8bits per color component.
        ///     <example>For example: 0xFF997755 where 0xFF is the alpha, 0x99 is the Red, 0x77 is the Green, 0x55 is the Blue.</example>
        /// </param>
        /// <returns>The registered <see cref="T:System.Windows.DependencyProperty" />.</returns>
        internal static DependencyProperty RegisterColor<TPalette>(string propertyName, long color) where TPalette : DependencyObject
        {
            var a = (byte) (color >> 24);
            var r = (byte) ((color & 16711680L) >> 16);
            var g = (byte) ((color & 65280L) >> 8);
            var b = (byte) (color & 255L);
            return DependencyProperty.Register(propertyName, typeof(Color), typeof(TPalette), new PropertyMetadata(Color.FromArgb(a, r, g, b), OnResourcePropertyChanged));
        }

        internal static DependencyProperty RegisterCornerRadius<TPalette>(string propertyName, double topLeft, double topRight, double bottomRight, double bottomLeft) => DependencyProperty.Register(propertyName, typeof(CornerRadius), typeof(TPalette), new PropertyMetadata(new CornerRadius(topLeft, topRight, bottomRight, bottomLeft), OnResourcePropertyChanged));

        internal static DependencyProperty RegisterDouble<TPalette>(string propertyName, double value) => DependencyProperty.Register(propertyName, typeof(double), typeof(TPalette), new PropertyMetadata(value, OnResourcePropertyChanged));

        internal static DependencyProperty RegisterDynamicCornerRadius<TPalette>(string propertyName, double valueLeft, double valueTop, double valueRight, double valueBottom) => DependencyProperty.Register(propertyName, typeof(CornerRadius), typeof(TPalette), new PropertyMetadata(new CornerRadius(valueLeft, valueTop, valueRight, valueBottom), OnDynamicCornerRadiusPropertyChanged));

        internal static DependencyProperty RegisterExternalFontFamily<TPalette>(string propertyName, string family, Uri uri) => DependencyProperty.Register(propertyName, typeof(FontFamily), typeof(TPalette), new PropertyMetadata(new FontFamily(uri, family), OnResourcePropertyChanged));

        internal static DependencyProperty RegisterFontFamily<TPalette>(string propertyName, string family) => DependencyProperty.Register(propertyName, typeof(FontFamily), typeof(TPalette), new PropertyMetadata(new FontFamily(family), OnResourcePropertyChanged));

        internal static DependencyProperty RegisterMasterDynamicColor<TPalette>(string propertyName, long color, PropertyChangedCallback propertyChangedCallback) => DependencyProperty.Register(propertyName, typeof(Color), typeof(TPalette), new PropertyMetadata(ColorFromLongValue(color), propertyChangedCallback));

        internal static DependencyProperty RegisterThickness<TPalette>(string propertyName, double valueLeft, double valueTop, double valueRight, double valueBottom) => DependencyProperty.Register(propertyName, typeof(Thickness), typeof(TPalette), new PropertyMetadata(new Thickness(valueLeft, valueTop, valueRight, valueBottom), OnResourcePropertyChanged));

        /// <summary>
        ///     Receives a semi-transparent color and a background color and returns a solid color (with 255 alpha)
        ///     <para>that is visually the same as the semi-transparent color when put on top of the background color.</para>
        /// </summary>
        /// <param name="targetColor">Semi-transparent color that needs to be converted to a solid color.</param>
        /// <param name="backgroundColor">The color that the target color is put on top of.</param>
        /// <returns>The calculated solid color.</returns>
        internal static Color RgbFromArgbAndBackgroundColor(Color targetColor, Color backgroundColor)
        {
            var num = targetColor.A / 255.0;
            var num2 = 1.0 - num;
            var r = (byte) (targetColor.R * num + backgroundColor.R * num2);
            var g = (byte) (targetColor.G * num + backgroundColor.G * num2);
            var b = (byte) (targetColor.B * num + backgroundColor.B * num2);
            return Color.FromArgb(byte.MaxValue, r, g, b);
        }

        internal static void SetResource(ResourceDictionary resourceDictionary, object key, object resource)
        {
            resourceDictionary[key] = resource;
        }

        internal static Color TransformAlpha(Color baseColor, byte alpha)
        {
            baseColor.A = alpha;
            return baseColor;
        }

        private static DependencyProperty GetBindingProperty(DependencyObject freezable)
        {
            if (freezable is SolidColorBrush)
            {
                return SolidColorBrush.ColorProperty;
            }

            if (freezable is GradientStop)
            {
                return GradientStop.ColorProperty;
            }

            return null;
        }

        private static void OnDynamicCornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is IDynamicThemePalette dynamicThemePalette)
            {
                if (e.NewValue != null && e.NewValue is CornerRadius radius)
                {
                    dynamicThemePalette.CornerRadiusBottom = new CornerRadius(0.0, 0.0, radius.BottomRight, radius.BottomLeft);
                    dynamicThemePalette.CornerRadiusLeft = new CornerRadius(radius.TopLeft, 0.0, 0.0, radius.BottomLeft);
                    dynamicThemePalette.CornerRadiusTop = new CornerRadius(radius.TopLeft, radius.TopRight, 0.0, 0.0);
                    dynamicThemePalette.CornerRadiusRight = new CornerRadius(0.0, radius.TopRight, radius.BottomRight, 0.0);
                }

                (d as ThemePalette)?.OnResourcePropertyChanged(e);
            }
        }

        private static void OnFreezableChanged(object sender, EventArgs e)
        {
            if (sender is Freezable freezable)
            {
                var bindingProperty = GetBindingProperty(freezable);
                var bindingExpression = BindingOperations.GetBindingExpression(freezable, bindingProperty);
                if (bindingExpression != null)
                {
                    freezable.Changed -= OnFreezableChanged;
                    bindingExpression.UpdateTarget();
                    freezable.SetValue(bindingProperty, freezable.GetValue(bindingProperty));
                    BindingOperations.ClearAllBindings(freezable);
                    freezable.Freeze();
                }
            }
        }

        private static void TryFreeze(Freezable freezable)
        {
            var bindingProperty = GetBindingProperty(freezable);
            if (bindingProperty != null)
            {
                var bindingExpression = BindingOperations.GetBindingExpression(freezable, bindingProperty);
                if (bindingExpression == null)
                {
                    freezable.Changed += OnFreezableChanged;
                    return;
                }

                bindingExpression.UpdateTarget();
                freezable.SetValue(bindingProperty, freezable.GetValue(bindingProperty));
                BindingOperations.ClearAllBindings(freezable);
                freezable.Freeze();
            }
        }
    }
}