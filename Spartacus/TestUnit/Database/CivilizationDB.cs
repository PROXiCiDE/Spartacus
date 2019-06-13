using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Dapper.Contrib.Extensions;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Civilizations;
using Spartacus.Logic;
using Spartacus.Logic.Builder.Civilizations;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;
using SpartacusUtils.SQLite.Annotations;

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

                Debug.WriteLine(conn.CreateTableSchema<Civilization>());

                civModel.DropTables(conn);
                civModel.CreateTables(conn);

                var civs = civModel.FromBar(new BarFileSystem(DataBar));
                Debug.WriteLine(civModel.InsertRepository(conn, civs));

                var civRep = civModel.FromRepository(conn).OrderBy( x=>x.CivilizationId);
                civRep.ForEach(civ =>
                {
                    Debug.WriteLine(civ);
                    Debug.WriteLine($"Shield: {civ.GetShieldTexture(ShieldTextureType.Enabled, CivilizationAgeTech.Age4)}, {civ.GetShieldTexture(ShieldTextureType.Disabled)}");
                });
            }
        }
    }
}