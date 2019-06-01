using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using ProjectCeleste.GameFiles.Tools.Bar;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using SpartacusUtils.Bar;
using SpartacusUtils.Models;
using SpartacusUtils.Models.Civilization;
using SpartacusUtils.Models.Milestone;
using SpartacusUtils.Xml.Helpers;

namespace SpartacusUtils.ConfigManager
{
    public class ConfigInfo : IConfigInfo
    {
        public Dictionary<BarFileEnum, BarFileReader> BarFileReaders { get; set; } = new Dictionary<BarFileEnum, BarFileReader>();

        public ConfigInfo()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            try
            {

                ConfigVariables();

                BarFileReaders.Add(BarFileEnum.Data, new BarFileReader(Path.Combine(Paths.Data, "Data.bar")));
                BarFileReaders.Add(BarFileEnum.ArtUI, new BarFileReader(Path.Combine(Paths.Art, "ArtUI.bar")));

                LoadBarItems();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private void ConfigVariables()
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
                SnackBarMessageDurationSeconds = 3;
        }

        public void LoadBarItems()
        {
            try
            {
                TechTree = TechTreeXml.FromXmlFile(Path.Combine(Paths.Data, "TechTreeX.xml"));
                LoadCivilization();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private void LoadCivilization()
        {
            var dataBar = BarFileReaders[BarFileEnum.Data];
            var findEntries = dataBar.FindEntries(@"civilizations\*.xmb");

            CivilizationDatas = new Dictionary<CivilizationEnum, CivilizationData>();

            foreach (var findEntry in findEntries)
            {
                var fileContents = dataBar.EntryToBytes(findEntry);
                var xmlFile = fileContents.EncodeXmlToString();
                if (xmlFile != null)
                {
                    var xmlClass = XmlUtils.DeserializeFromXml<CivilizationXml>(xmlFile);

                    if (int.TryParse(xmlClass.Displaynameid, out var displayNameId))
                        CivilizationDatas.Add(
                            xmlClass.Civid,
                            new CivilizationData(
                                xmlClass.Name,
                                xmlClass.Civmatchingid,
                                displayNameId,
                                new CivilizationShieldData(xmlClass.Ui.Shieldtexture,
                                    xmlClass.Ui.Shieldgreytexture),
                                new StartingResourcesData(xmlClass.Startingresources),
                                new MilestoneRewardsModel(this, xmlClass.Civid)));
                    else
                    {
                        throw new Exception("CivilizationXml : Element 'DisplayName' in-explicit conversion of Integer");
                    }
                }
            }
        }


        public Dictionary<CivilizationEnum, CivilizationData> CivilizationDatas { get; set; }
        public double SnackBarMessageDurationSeconds { get; set; }
        public MilestoneRewardsModel MilestoneRewardsModel { get; set; }
        public string DataBarPath { get; set; }
        public string ArtUiPath { get; set; }
        public TechTreeXml TechTree { get; set; }
        public ConfigInfoPaths Paths { get; set; }
    }
}
