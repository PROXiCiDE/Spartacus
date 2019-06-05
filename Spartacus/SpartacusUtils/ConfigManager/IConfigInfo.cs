using System.Collections.Generic;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Bar;

namespace SpartacusUtils.ConfigManager
{
    public interface IConfigInfo
    {
        double SnackBarMessageDurationSeconds { get; set; }
        string DataBarPath { get; set; }
        string ArtUiPath { get; set; }
        TechTreeXml TechTree { get; set; }

        ConfigInfoPaths Paths { get; set; }
        Dictionary<BarFileEnum, BarFileSystem> BarFileReaders { get; set; }

        LanguagesXml Languages { get; set; }
    }
}