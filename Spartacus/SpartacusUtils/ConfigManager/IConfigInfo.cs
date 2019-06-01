using System.Collections.Generic;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Models;

namespace SpartacusUtils.ConfigManager
{
    public interface IConfigInfo
    {
        List<CivilizationData> CivilizationDatas { get; set; }
        double SnackBarMessageDurationSeconds { get; set; }
        MilestoneRewardsModel MilestoneRewardsModel { get; set; }
        string DataBarPath { get; set; }
        string ArtUiPath { get; set; }
        TechTreeXml TechTree { get; set; }

        ConfigInfoPaths Paths { get; set; }
    }
}