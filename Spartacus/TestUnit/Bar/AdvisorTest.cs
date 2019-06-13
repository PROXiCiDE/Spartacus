using NUnit.Framework;
using Spartacus.Logic.Builder.Advisors;
using SpartacusUtils.Bar;

namespace TestUnit.Bar
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

            var modelBuilder = new AdvisorBuilder();
            var models = modelBuilder.FromBar(dataBar);
            //foreach (var advisor in models.Where(x => x.Civid == (long) CivilizationEnum.Norse))
                //Debug.WriteLine(advisor);
        }
    }
}