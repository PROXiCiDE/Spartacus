using System.Diagnostics;
using NUnit.Framework;
using SpartacusUtils.Bar;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string barFile = @"G:\Age of Empires Online\Data2\data.bar";
            BarFileReader barFileReader = new BarFileReader(barFile);
            var entries = barFileReader.FindEntries(@"civ*\*.xmb");
            foreach (var barEntry in entries)
            {
                Debug.WriteLine(barEntry.FileName);
            }

            var entry = barFileReader.GetEntry("milestonerewards.xml.xmb");
            Debug.WriteLine(entry.FileName);
        }
    }
}