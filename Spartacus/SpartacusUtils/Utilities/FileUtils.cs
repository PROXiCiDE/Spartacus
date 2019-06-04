using System.IO;
using System.Linq;

namespace SpartacusUtils.Utilities
{
    public static class FileUtils
    {
        public static bool IsXmlFile(string file)
        {
            var contents = File.ReadAllText(file).Take(50);
            var str = new string(contents.ToArray());
            if (!string.IsNullOrEmpty(str) && str.TrimStart().StartsWith("<"))
                return true;
            return false;
        }
    }
}
