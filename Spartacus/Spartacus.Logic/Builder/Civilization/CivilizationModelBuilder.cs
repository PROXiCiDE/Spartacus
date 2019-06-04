using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using Spartacus.Database.DBModels.Civilizations;
using SpartacusUtils.Bar;
using SpartacusUtils.Xml.Helpers;

namespace Spartacus.Logic.Builder.Civilization
{
    public class CivilizationModelBuilder
    {
        public List<Civilizations> BuildFromBar(BarFileReader barFileReader)
        {
            var findEntries = barFileReader.FindEntries(@"civilizations\*.xmb");

            var civilizationDatas = new List<Civilizations>();

            foreach (var findEntry in findEntries)
            {
                var fileContents = barFileReader.EntryToBytes(findEntry);
                var xmlFile = fileContents.EncodeXmlToString();
                if (xmlFile != null)
                {
                    var xmlClass = XmlUtils.DeserializeFromXml<CivilizationXml>(xmlFile);

                    if (int.TryParse(xmlClass.Displaynameid, out var displayNameId) &&
                        int.TryParse(xmlClass.Rollovernameid, out var rollOverId))
                        if (int.TryParse(xmlClass.Ui.Storehousetechid, out var storageTechId))
                        {
                            var age0 = xmlClass.Agetech?.FirstOrDefault(x =>
                                               string.Equals(x.Age, "age0", StringComparison.OrdinalIgnoreCase))
                                           ?.Tech ?? "";
                            var age1 = xmlClass.Agetech?.FirstOrDefault(x =>
                                               string.Equals(x.Age, "age1", StringComparison.OrdinalIgnoreCase))
                                           ?.Tech ?? "";
                            var age2 = xmlClass.Agetech?.FirstOrDefault(x =>
                                               string.Equals(x.Age, "age2", StringComparison.OrdinalIgnoreCase))
                                           ?.Tech ?? "";
                            var age3 = xmlClass.Agetech?.FirstOrDefault(x =>
                                               string.Equals(x.Age, "age3", StringComparison.OrdinalIgnoreCase))
                                           ?.Tech ?? "";

                            civilizationDatas.Add(
                                new Civilizations(
                                    (long) xmlClass.Civid,
                                    displayNameId,
                                    rollOverId,
                                    xmlClass.Ui.Shieldtexture,
                                    xmlClass.Ui.Shieldgreytexture,
                                    age0, age1, age2, age3, storageTechId));
                        }
                        else
                        {
                            throw new Exception(
                                "CivilizationXml : Element 'DisplayName' in-explicit conversion of Integer");
                        }
                }
            }

            return civilizationDatas;
        }
    }
}