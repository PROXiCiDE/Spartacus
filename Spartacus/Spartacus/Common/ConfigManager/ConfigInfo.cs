using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using SpartacusUtils.Bar;
using SpartacusUtils.Xml.Helpers;

namespace Spartacus.Common.ConfigManager
{
    public static class ConfigInfo
    {
        public static List<CivilizationData> CivilizationList;
        public static double SnackBarMessageDurationSeconds;

        public static void LoadConfig()
        {
            var timespan = ConfigurationManager.AppSettings["SnackBarMessageDurationSeconds"];
            if (double.TryParse(timespan, out var timespanResults))
                SnackBarMessageDurationSeconds = timespanResults;
            else
            {
                SnackBarMessageDurationSeconds = 3;
            }

            LoadBarItems(ConfigurationManager.AppSettings["DataBar"]);
        }

        public static void LoadBarItems(string dataBar)
        {
            try
            {
                LoadCivilization(dataBar);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private static void LoadCivilization(string dataBar)
        {
            BarFileReader barFileReader = new BarFileReader(dataBar);
            var findEntries = barFileReader.FindEntries(@"civilizations\*.xmb");

            CivilizationList = new List<CivilizationData>();
            foreach (var findEntry in findEntries)
            {
                var fileContents = barFileReader.ToBytes(findEntry);
                var xmlFile = fileContents.EncodeXmlToString();
                if (xmlFile != null)
                {
                    var xmlClass = XmlUtils.DeserializeFromXml<CivilizationXml>(xmlFile);
                    CivilizationList.Add(new CivilizationData(xmlClass.Civid, xmlClass.Name));
                }
            }
        }
    }
}
