using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using Spartacus.Logic.Builder.Advisors;
using SpartacusUtils.Bar;

namespace TestUnit
{
    class AdvisorTest : TestBarBase
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void LoadAdvisor()
        {
            var dataBar = new BarFileReader(@"G:\Age of Empires Online\Data2\data.bar");

            AdvisorModelBuilder modelBuilder = new AdvisorModelBuilder();
            var models = modelBuilder.BuildFromBar(dataBar);
            foreach (var advisor in models.Where(x=>x.Civid == (long) CivilizationEnum.Norse))
            {
                Debug.WriteLine(advisor);
            }
        }
    }
}
