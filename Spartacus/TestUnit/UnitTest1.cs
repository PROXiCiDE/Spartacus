using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using SpartacusUtils.Bar;
using SpartacusUtils.ConfigManager;
using SpartacusUtils.Xml.Language;

namespace TestUnit
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
        public void ShowStringXmbs()
        {
            var databar = _configInfo.BarFileReaders[BarFileEnum.Data];
            var entries = databar.FindEntries("*strings*.xmb");
            foreach (var entry in entries)
            {
                Debug.WriteLine(entry.FileName);
            }
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
                {
                    Debug.WriteLine($"{keyValuePair.Key}, {keyValuePair.Value.LocId}, {keyValuePair.Value.Text}");
                }
            }

            Debug.WriteLine(languages["stringtablex"].Language["English"].LanguageString[11723].Text);

            foreach (var languageReaderError in errors.Take(5))
            {
                Debug.WriteLine(languageReaderError.Exception);
            }
        }

        [Test]
        public void LoadLanguagesXml_NonBar()
        {
            var languages = LanguagesXml.LanguagesFromXmlFiles(@"G:\Age of Empires Online\Data");
            foreach (var stringTableXml in languages.Gets())
            {
                Debug.WriteLine(stringTableXml.Id);
            }
        }

        [Test]
        public void TestProtoUnit()
        {
            Debug.WriteLine("Test");
            foreach (var stringTableXml in _configInfo.Languages.Gets())
            {
                Debug.WriteLine(stringTableXml);
            }
        }
    }
}