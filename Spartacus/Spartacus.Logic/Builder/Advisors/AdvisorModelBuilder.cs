using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Advisors;
using SpartacusUtils.Bar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartacusUtils.Utilities;

namespace Spartacus.Logic.Builder.Advisors
{
    public class AdvisorModelBuilder
    {
        public List<AdvisorsModel> BuildFromBar(BarFileReader barFileReader)
        {
            var models = new List<AdvisorsModel>();

            var entry = barFileReader.GetEntry(@"advisors.xml");
            if(entry != null)
            {
                var xmlClass = barFileReader.ReadEntry<EconAdvisorsXml>(entry);

                xmlClass?.AdvisorArray.ForEach(x =>
                {
                    var rolloverTextId = 0;
                    if (int.TryParse(x.RollOverTextId, out var rolloverTemp))
                        rolloverTextId = rolloverTemp;

                    models.Add(new AdvisorsModel(
                        x.Name,(long) x.Civilization,x.Age,
                        x.Icon,(long) x.Rarity, rolloverTextId,
                        x.DisplayDescriptionId,
                        x.DisplayNameId,x.Minlevel,x.ItemLevel,
                        x.Techs.Tech
                    ));
                });
            }
            return models;
        }
    }
}
