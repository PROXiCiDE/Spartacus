using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartacusUtils.Utilities
{
    public static class MiscExtensions
    {
        /// <summary>
        /// ForEach for IEnumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
    }
}
