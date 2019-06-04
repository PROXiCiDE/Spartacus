using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using ProjectCeleste.GameFiles.XMLParser.Enum;
using ProjectCeleste.GameFiles.XMLParser.Helpers;
using Spartacus.Database.DBModels.Civilizations;
using Spartacus.Database.DBModels.FileChecksum;
using Spartacus.Logic.Builder.Civilization;
using SpartacusUtils.Bar;
using SpartacusUtils.Xml.Helpers;

namespace TestUnit
{
    class UnitTestDapper
    {
        private string _dataFile = @"G:\MS Age Of Empires Online\DATA\data.bar";
        public string ConnString { get; set; }

        [SetUp]
        public void Setup()
        {
            ConnString = @"Data Source=G:\_DB Dev\Spartacus.db;Version=3;";
        }

        [Test]
        public void InsertChecksum()
        {
            FileChecksumRepository fileChecksum = new FileChecksumRepository(ConnString);
            var time = DateTime.Now;

            var newChecksum = new FileChecksumModel("TestFile2.xml", @"\Data\TestFile2.xml", "ABC", time.AddMinutes(-15).ToFileTimeUtc());
            fileChecksum.InsertFileChecksum(newChecksum);
        }

        [Test]
        public void SelectChecksun()
        {
            FileChecksumRepository fileChecksum = new FileChecksumRepository(ConnString);
            var checksums = fileChecksum.SelectFileChecksum();
            foreach (var checksum in checksums)
            {
                Debug.WriteLine(checksum);
            }
        }

        [Test]
        public void InsertCivilization()
        {
            CivilizationsRepository civilizations = new CivilizationsRepository(ConnString);
            var civs = new CivilizationModelBuilder().BuildFromBar(new BarFileReader(_dataFile));

            foreach (var civ in civs)
            {
                Debug.WriteLine(civ);
            }
        }
    }
}
