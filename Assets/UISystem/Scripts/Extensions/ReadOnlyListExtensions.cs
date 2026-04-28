using System.Collections.Generic;

namespace UISystem.Extensions
{
    /// <summary>
    /// Class containing IReadOnlyList extensions.
    /// </summary>
    internal static class ReadOnlyListExtensions
    {
        /// <summary>
        /// Finds the index of an item.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="list">List to search.</param>
        /// <param name="item">Item to search for.</param>
        /// <returns>Index of an item or -1 if list does not contain it.</returns>
        public static int IndexOf<T>(this IReadOnlyList<T> list, T item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(list[i], item))
                    return i;
            }

            return -1;
        }
    }
}
