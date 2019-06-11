using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using Spartacus.Logic.Builder.Advisors;
using SpartacusUtils.Bar;

namespace TestUnit
{
    internal class AdvisorTest : TestBarBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LoadAdvisor()
        {
            var dataBar = new BarFileSystem(@"G:\Age of Empires Online\Data2\data.bar");

            var modelBuilder = new AdvisorModelBuilder();
            var models = modelBuilder.FromBar(dataBar);
            //foreach (var advisor in models.Where(x => x.Civid == (long) CivilizationEnum.Norse))
                //Debug.WriteLine(advisor);
        }
    }
}