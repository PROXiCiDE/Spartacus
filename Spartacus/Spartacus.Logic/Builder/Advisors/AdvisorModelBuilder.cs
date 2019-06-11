using System;
using System.Collections.Generic;
using System.Data;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Advisors;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;

namespace Spartacus.Logic.Builder.Advisors
{
    public class AdvisorModelBuilder : IModelBuilder<AdvisorsModel>
    {
        public List<AdvisorsModel> FromBar(BarFileSystem barFileReader)
        {
            var models = new List<AdvisorsModel>();

            var entry = barFileReader.GetEntry(StringResource.XmlFile_Advisors);
            if (entry != null)
            {
                var xmlClass = barFileReader.ReadEntry<EconAdvisorsXml>(entry);

                xmlClass?.AdvisorArray.ForEach(x =>
                {
                    var rolloverTextId = 0;
                    if (int.TryParse(x.RollOverTextId, out var rolloverTemp))
                        rolloverTextId = rolloverTemp;

                    models.Add(new AdvisorsModel(
                        x.Name, (long) x.Civilization, x.Age,
                        x.Icon, (long) x.Rarity, rolloverTextId,
                        x.DisplayDescriptionId,
                        x.DisplayNameId, x.Minlevel, x.ItemLevel,
                        x.Techs.Tech
                    ));
                });
            }

            return models;
        }

        public List<AdvisorsModel> FromRepository(IDbConnection connection)
        {
            throw new NotImplementedException();
        }
    }
}