using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace SpartacusUtils.Utilities
{
    public static class PathUtils
    {
        /// <summary>
        /// Removes the leading dot in the file extension
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns>string of filename without a leading dot</returns>
        public static string GetExtensionWithoutDot(string filename)
        {
            return Path.GetExtension(filename)?.Replace(".", "");
        }

        /// <summary>
        /// Will return the path only
        /// </summary>
        /// <param name="path"></param>
        /// <remarks>If no path is present then it will return the filename</remarks>
        /// <returns>Directory path string</returns>
        public static string GetPathOnly(string path)
        {
            var dirSep = Path.DirectorySeparatorChar.ToString();
            var parts = path.Split(new string[] { dirSep }, StringSplitOptions.None);
            var count = parts.Count();
            if (count >= 1)
                count -= 1;
            return string.Join(dirSep, parts.Take(count).ToArray());
        }
    }
}
