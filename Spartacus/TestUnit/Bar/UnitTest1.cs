using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Bar;
using SpartacusUtils.ConfigManager;
using SpartacusUtils.Utilities;
using SpartacusUtils.Xml.Language;

namespace TestUnit.Bar
{
    public class Tests
    {
        private ConfigInfo _configInfo;

        [SetUp]
        public void Setup()
        {
            _configInfo = new ConfigInfo(@"G:\MS Age Of Empires Online");
        }

        [Test]
        [TestCase("xml")]
        [TestCase("shp")]
        [TestCase("eee")]
        public void CheckExt(string ext)
        {
            var extList = new List<string>
            {
                "region",
                "xml",
                "dataset",
                "character",
                "tactics",
                "SpawnerItem",
                "blueprint",
                "physics",
                "shp",
                "quest",
                "xsd",
                "cpn",
                "dtd"
            };
            Debug.WriteLine(extList.Contains(ext));
        }

        [Test]
        public void TestPathSplit()
        {
            //var path = @"\thisisareallylongname.xml.xmb";
            var path = @"d:\data\data2\data3\data5\thisisareallylongname.xml.xmb";
            //Debug.WriteLine(GetLastParts(path, Path.DirectorySeparatorChar.ToString(), 2));
            Debug.WriteLine(PathUtils.CleanPath(path));
        }


        [Test]
        public void ShowStringXmbs()
        {
            var databar = _configInfo.BarFileReaders[BarFileEnum.Data];
            var entries = databar.FindEntries("*strings*.xmb");
            foreach (var entry in entries) Debug.WriteLine(entry.FileName);
        }

        [Test]
        public void LoadLanguagesXml()
        {
            var databar = _configInfo.BarFileReaders[BarFileEnum.Data];
            var languages = LanguagesReader.FromBarFile(databar, out var errors, true);

            foreach (var stringTableXml in languages.Gets().Take(5))
            {
                Debug.WriteLine($"{stringTableXml.Id}");

                var langTable = stringTableXml.Language["English"];
                foreach (var keyValuePair in langTable.LanguageString.Take(5))
                    Debug.WriteLine($"{keyValuePair.Key}, {keyValuePair.Value.LocId}, {keyValuePair.Value.Text}");
            }

            Debug.WriteLine(languages["stringtablex"].Language["English"].LanguageString[11723].Text);

            foreach (var languageReaderError in errors.Take(5)) Debug.WriteLine(languageReaderError.Exception);
        }

        [Test]
        public void LoadLanguagesXml_NonBar()
        {
            var languages = LanguagesXml.LanguagesFromXmlFiles(@"G:\Age of Empires Online\Data");
            foreach (var stringTableXml in languages.Gets()) Debug.WriteLine(stringTableXml.Id);
        }

        [Test]
        public void TestProtoUnit()
        {
            Debug.WriteLine("Test");
            foreach (var stringTableXml in _configInfo.Languages.Gets()) Debug.WriteLine(stringTableXml);
        }
    }
}