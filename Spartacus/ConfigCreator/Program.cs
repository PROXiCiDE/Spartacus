using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ProjectCeleste.GameFiles.Tools.Bar;
using ProjectCeleste.GameFiles.Tools.Xmb;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using SpartacusUtils;
using SpartacusUtils.Bar;
using SpartacusUtils.Xml.Helpers;

namespace ConfigCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BarFileReader barFile = new BarFileReader(@"G:\Age of Empires Online\Data2\data.bar");
                IEnumerable<BarEntry> findEntries = barFile.FindEntries(@"civilizations\*.xmb");

                foreach (var findEntry in findEntries)
                {
                    Debug.WriteLine(findEntry.FileName);
                }

                var array = findEntries.ToArray();
                var file = barFile.ToBytes(array[0]);

                var xmlfile = file.EncodeXmlToString();
                var xml = XmlUtils.DeserializeFromXml<CivilizationXml>(xmlfile);
                if (xml != null) Debug.WriteLine($"CivID: {xml.Civid} Name: {xml.Name}");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
