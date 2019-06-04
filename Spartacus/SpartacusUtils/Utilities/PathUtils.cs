using System.IO;

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
    }
}
