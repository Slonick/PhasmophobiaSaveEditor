using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace PhasmophobiaSaveEditor.Extensions
{
    /// <summary>
    ///     Contains extension methods for enumerating the parents of an element.
    /// </summary>
    public static class ParentOfTypeExtensions
    {
        /// <summary>
        ///     Enumerates through element's parents in the visual tree.
        /// </summary>
        public static IEnumerable<DependencyObject> GetParents(this DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            while ((element = element.GetParent()) != null)
            {
                yield return element;
            }
        }

        /// <summary>
        ///     Searches up in the visual tree for parent element of the specified type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the parent that will be searched up in the visual object hierarchy.
        ///     The type should be <see cref="T:System.Windows.DependencyObject"/>.
        /// </typeparam>
        /// <param name="element">
        ///     The target <see cref="T:System.Windows.DependencyObject"/> which visual parents will be
        ///     traversed.
        /// </param>
        /// <returns>Visual parent of the specified type if there is any, otherwise null.</returns>
        public static T GetVisualParent<T>(this DependencyObject element) where T : DependencyObject => element.ParentOfType<T>();

        /// <summary>
        ///     Determines whether the element is an ancestor of the descendant.
        /// </summary>
        /// <returns>true if the visual object is an ancestor of descendant; otherwise, false.</returns>
        public static bool IsAncestorOf(this DependencyObject element, DependencyObject descendant)
        {
            element.TestNotNull(nameof(element));
            descendant.TestNotNull(nameof(descendant));
            return descendant == element || descendant.GetParents().Contains(element);
        }

        /// <summary>
        ///     Gets the parent element from the visual tree by given type.
        /// </summary>
        public static T ParentOfType<T>(this DependencyObject element) where T : DependencyObject => element?.GetParents().OfType<T>().FirstOrDefault();

        /// <summary>
        ///     This recurse the visual tree for ancestors of a specific type.
        /// </summary>
        internal static IEnumerable<T> GetAncestors<T>(this DependencyObject element) where T : class => element.GetParents().OfType<T>();

        /// <summary>
        ///     This recurse the visual tree for a parent of a specific type.
        /// </summary>
        internal static T GetParent<T>(this DependencyObject element) where T : FrameworkElement => element.ParentOfType<T>();

        private static DependencyObject GetParent(this DependencyObject element)
        {
            DependencyObject dependencyObject;
            try
            {
                dependencyObject = VisualTreeHelper.GetParent(element);
            }
            catch (InvalidOperationException)
            {
                dependencyObject = null;
            }

            if (dependencyObject != null)
            {
                return dependencyObject;
            }

            switch (element)
            {
                case FrameworkElement frameworkElement:
                    dependencyObject = frameworkElement.Parent;
                    break;
                case FrameworkContentElement frameworkContentElement:
                    dependencyObject = frameworkContentElement.Parent;
                    break;
            }

            return dependencyObject;
        }
    }
}