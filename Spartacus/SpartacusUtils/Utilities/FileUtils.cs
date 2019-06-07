using System.IO;
using SpartacusUtils.Helpers;

namespace SpartacusUtils.Utilities
{
    public static class FileUtils
    {
        /// <summary>
        ///     Generic XML File Checker,by default it takes the first 50 bytes
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="readCount"></param>
        /// <returns>Boolean</returns>
        public static bool IsXmlFile(string filename, int readCount = 50)
        {
            if (!File.Exists(filename))
                return false;

            using (var file = File.OpenRead(filename))
            {
                using (var reader = new BinaryReader(file))
                {
                    var encoding = reader.GetEncoding();
                    var contents = new byte[readCount];

                    reader.Read(contents, 0, readCount);

                    var str = encoding.GetString(contents);
                    if (!string.IsNullOrEmpty(str) && str.TrimStart().StartsWith("<"))
                        return true;
                }
            }

            return false;
        }
    }
}