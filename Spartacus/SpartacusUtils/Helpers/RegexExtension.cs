using System.Text.RegularExpressions;

namespace SpartacusUtils.Helpers
{
    public static class RegexExtension
    {
        public static string ToWildCard(this string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }
    }
}