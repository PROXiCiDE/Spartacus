using System;
using System.Collections.Generic;

namespace SpartacusUtils.Helpers
{
    public static class MiscExtensions
    {
        /// <summary>
        ///     ForEach for IEnumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }
    }
}