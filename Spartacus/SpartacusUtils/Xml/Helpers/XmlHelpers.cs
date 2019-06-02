using System.Text;
using ProjectCeleste.GameFiles.Tools.Xmb;

namespace SpartacusUtils.Xml.Helpers
{
    public static class XmlHelpers
    {
        public static string EncodeXmlToString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(XmbFileUtils.XmbToXml(bytes));
        }
    }
}