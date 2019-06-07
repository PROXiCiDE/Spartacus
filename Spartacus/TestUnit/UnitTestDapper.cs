using System;
using System.Diagnostics;
using NUnit.Framework;
using Spartacus.Database.DBModels.Civilizations;
using Spartacus.Database.DBModels.FileChecksum;
using Spartacus.Logic.Builder.Civilization;
using SpartacusUtils.Bar;

namespace TestUnit
{
    internal class UnitTestDapper
    {
        private readonly string _dataFile = @"G:\MS Age Of Empires Online\DATA\data.bar";
        public string ConnString { get; set; }

        [SetUp]
        public void Setup()
        {
            ConnString = @"Data Source=G:\_DB Dev\Spartacus.db;Version=3;";
        }

        [Test]
        public void InsertChecksum()
        {
            var fileChecksum = new FileChecksumRepository(ConnString);
            var time = DateTime.Now;

            var newChecksum = new FileChecksumModel("TestFile2.xml", @"\Data\TestFile2.xml", "ABC",
                time.AddMinutes(-15).ToFileTimeUtc());
            fileChecksum.InsertFileChecksum(newChecksum);
        }

        [Test]
        public void SelectChecksun()
        {
            var fileChecksum = new FileChecksumRepository(ConnString);
            var checksums = fileChecksum.SelectFileChecksum();
            foreach (var checksum in checksums) Debug.WriteLine(checksum);
        }

        [Test]
        public void InsertCivilization()
        {
            var civilizations = new CivilizationsRepository(ConnString);
            var civs = new CivilizationModelBuilder().FromBar(new BarFileSystem(_dataFile));

            foreach (var civ in civs) Debug.WriteLine(civ);
        }
    }
}