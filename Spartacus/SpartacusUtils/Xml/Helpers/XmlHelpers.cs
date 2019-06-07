using System.Text;
using ProjectCeleste.GameFiles.Tools.Xmb;

namespace SpartacusUtils.Xml.Helpers
{
    public static class XmlHelpers
    {
        public static string EncodeXmlToString(this byte[] bytes)
        {
            if (bytes.Length > 0)
            {
                if (bytes[0] == 0x58 && bytes[1] == 0x31)
                    return Encoding.UTF8.GetString(XmbFileUtils.XmbToXml(bytes));
                return Encoding.UTF8.GetString(bytes);
            }

            return null;
        }
    }
}