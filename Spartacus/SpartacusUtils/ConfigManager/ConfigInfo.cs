using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Bar;
using SpartacusUtils.Xml.Language;

namespace SpartacusUtils.ConfigManager
{
    public class ConfigInfo : IConfigInfo
    {
        public ConfigInfo(string gamePath = null)
        {
            LoadConfig(gamePath);
        }

        public Dictionary<BarFileEnum, BarFileReader> BarFileReaders { get; set; } =
            new Dictionary<BarFileEnum, BarFileReader>();

        public LanguagesXml Languages { get; set; }

        public double SnackBarMessageDurationSeconds { get; set; }
        public string DataBarPath { get; set; }
        public string ArtUiPath { get; set; }
        public TechTreeXml TechTree { get; set; }
        public ConfigInfoPaths Paths { get; set; }


        public void LoadConfig(string gamePath = null)
        {
            try
            {
                ConfigVariables(gamePath);
                LoadBarItems();
                //LoadLanguage();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private void LoadLanguage()
        {
            Languages = LanguagesReader.FromBarFile(BarFileReaders[BarFileEnum.Data], out var errors);
        }

        private void ConfigVariables(string gamePath = null)
        {
            //Setup paths, check for custom paths we might be working with
            if (string.IsNullOrEmpty(gamePath))
                gamePath = ConfigurationManager.AppSettings["GameInstallPath"];
            Paths = new ConfigInfoPaths(gamePath);

            //BarFileReader Enums
            BarFileReaders.Add(BarFileEnum.Data, new BarFileReader(Path.Combine(Paths.Data, "Data.bar")));
            BarFileReaders.Add(BarFileEnum.ArtUI, new BarFileReader(Path.Combine(Paths.Art, "ArtUI.bar")));

            DataBarPath = ConfigurationManager.AppSettings["Data_Bar"];
            if (string.IsNullOrEmpty(DataBarPath))
                DataBarPath = Path.Combine(gamePath, @"Data\Data.bar");

            ArtUiPath = ConfigurationManager.AppSettings["ArtUI_Bar"];
            if (string.IsNullOrEmpty(ArtUiPath))
                ArtUiPath = Path.Combine(gamePath, @"Art\ArtUi.bar");

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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}