using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Civilizations;
using Spartacus.Logic;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;

namespace TestUnit.Database
{
    public class CivilizationDB : SqliteTestBase
    {
        [Test]
        public void TestCivilization()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                conn.DropTableIfExists<Civilization>();
                //Debug.WriteLine(conn.CreateTableSchema<Civilization>());
                conn.CreateTable<Civilization>();
                var civs = FromBar(new BarFileSystem(DataBar));
                civs.ForEach(civ =>
                {
                    Debug.WriteLine(civ);
                });
            }
        }

        public List<Civilization> FromBar(BarFileSystem barFileReader)
        {
            var findEntries = barFileReader.FindEntries(@"civilizations\*.xmb");

            var civilizations = new List<Civilization>();
            findEntries.ForEach(findEntry =>
            {
                var xmlClass = barFileReader.ReadEntry<CivilizationXml>(findEntry);
                if (xmlClass != null)
                {
                    Debug.WriteLine(xmlClass.Name);
                    civilizations.Add(new Civilization(xmlClass));
                }
                else
                {
                    throw new Exception(
                        "CivilizationXml : Element 'DisplayName' in-explicit conversion of Integer");
                }
            });

            return civilizations;
        }

    }
}