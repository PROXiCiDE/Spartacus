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
using Spartacus.Logic.Builder.Civilizations;
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
                var civModel = new CivilizationModelBuilder();

                civModel.DropTables(conn);
                civModel.CreateTables(conn);

                var civs = civModel.FromBar(new BarFileSystem(DataBar));
                Debug.WriteLine(civModel.InsertRepository(conn, civs));

                var civRep = civModel.FromRepository(conn);
                civRep.ForEach(civ =>
                {
                    Debug.WriteLine(civ);
                });
            }
        }



    }
}