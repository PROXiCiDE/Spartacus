using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using SpartacusUtils.Bar;
using SpartacusUtils.Models;
using SpartacusUtils.Xml.Helpers;

namespace SpartacusUtils.ConfigManager
{
    public class ConfigInfo : IConfigInfo
    {
        public ConfigInfo()
        {
            LoadConfig();
        }

        public  void LoadConfig()
        {
            try
            {
                //Setup paths, check for custom paths we might be working with
                var gamepath = ConfigurationManager.AppSettings["GameInstallPath"];
                Paths = new ConfigInfoPaths(gamepath);

                DataBarPath = ConfigurationManager.AppSettings["Data_Bar"];
                if (string.IsNullOrEmpty(DataBarPath))
                    DataBarPath = Path.Combine(gamepath, @"Data\Data.bar");

                ArtUiPath = ConfigurationManager.AppSettings["ArtUI_Bar"];
                if (string.IsNullOrEmpty(ArtUiPath))
                    ArtUiPath = Path.Combine(gamepath, @"Art\ArtUi.bar");

                var timespan = ConfigurationManager.AppSettings["SnackBarMessageDurationSeconds"];
                if (double.TryParse(timespan, out var timespanResults))
                    SnackBarMessageDurationSeconds = timespanResults;
                else
                {
                    SnackBarMessageDurationSeconds = 3;
                }

                LoadBarItems(DataBarPath);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public  void LoadBarItems(string dataBar)
        {
            try
            {
                TechTree = TechTreeXml.FromXmlFile(Path.Combine(Paths.Data, "TechTreeX.xml"));
                LoadCivilization(dataBar);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private  void LoadCivilization(string dataBar)
        {
            BarFileReader barFileReader = new BarFileReader(dataBar);
            var findEntries = barFileReader.FindEntries(@"civilizations\*.xmb");

            CivilizationDatas = new List<CivilizationData>();
            foreach (var findEntry in findEntries)
            {
                var fileContents = barFileReader.ToBytes(findEntry);
                var xmlFile = fileContents.EncodeXmlToString();
                if (xmlFile != null)
                {
                    var xmlClass = XmlUtils.DeserializeFromXml<CivilizationXml>(xmlFile);
                    CivilizationDatas.Add(new CivilizationData(xmlClass.Civid, xmlClass.Name));
                }
            }
        }

        public List<CivilizationData> CivilizationDatas { get; set; }
        public double SnackBarMessageDurationSeconds { get; set; }
        public MilestoneRewardsModel MilestoneRewardsModel { get; set; }
        public string DataBarPath { get; set; }
        public string ArtUiPath { get; set; }
        public TechTreeXml TechTree { get; set; }
        public ConfigInfoPaths Paths { get; set; }
    }
}
