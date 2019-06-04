using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
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
            var entry = dataBar.GetEntry("advisors.xml");
            var advisors = dataBar.ReadEntry<EconAdvisorsXml>(entry);
            foreach (var advisor in advisors.AdvisorArray)
            {
                if(advisor.Age > 0)
                    Debug.WriteLine(advisor.Name);
            }
        }
    }
}
