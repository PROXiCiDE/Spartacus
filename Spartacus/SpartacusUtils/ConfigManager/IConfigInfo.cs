using System.Collections.Generic;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using SpartacusUtils.Bar;
using SpartacusUtils.Models;
using SpartacusUtils.Models.Civilization;
using SpartacusUtils.Models.Milestone;

namespace SpartacusUtils.ConfigManager
{
    public interface IConfigInfo
    {
        Dictionary<CivilizationEnum, CivilizationData> CivilizationDatas { get; set; }
        double SnackBarMessageDurationSeconds { get; set; }
        MilestoneRewardsModel MilestoneRewardsModel { get; set; }
        string DataBarPath { get; set; }
        string ArtUiPath { get; set; }
        TechTreeXml TechTree { get; set; }

        ConfigInfoPaths Paths { get; set; }
        Dictionary<BarFileEnum, BarFileReader> BarFileReaders { get; set; }
    }
}