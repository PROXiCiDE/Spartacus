using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
