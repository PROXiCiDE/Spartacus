using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Civilizations;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;

namespace Spartacus.Logic.Builder.Civilizations
{
    public class CivilizationModelBuilder : IRepositoryBuilder<Civilization>
    {
        /// <inheritdoc />
        public IEnumerable<Civilization> FromBar(BarFileSystem barFileReader)
        {
            var findEntries = barFileReader.FindEntries(StringResource.XmlFile_CivilizationPattern);
            var civilizations = new List<Civilization>();
            findEntries.ForEach(findEntry =>
            {
                var xmlClass = barFileReader.ReadEntry<CivilizationXml>(findEntry);
                if (xmlClass != null)
                    civilizations.Add(new Civilization(xmlClass));
                else
                    throw new Exception(string.Format(StringResource.Exception_XmlFileRead, findEntry.FileName));
            });

            return civilizations;
        }

        /// <inheritdoc />
        public IEnumerable<Civilization> FromRepository(IDbConnection connection)
        {
            var sqlCommand = "SELECT * FROM Civilization AS civilization " +
                             "INNER JOIN CivilizationStartingResource AS resource " +
                             "ON civilization.CivilizationId = resource.CivId;";
            return connection.Query<Civilization, CivilizationStartingResource, Civilization>(sqlCommand,
                (civilization, resource) =>
                {
                    civilization.StartingResource = resource;
                    return civilization;
                }, splitOn: "CivId").ToList();
        }

        /// <inheritdoc />
        public long InsertRepository(IDbConnection connection, IEnumerable<Civilization> entities)
        {
            long writeCivCount = 0;
            long writeResCount = 0;
            connection.Open();

            var resources = new List<CivilizationStartingResource>();


            using (var trans = connection.BeginTransaction())
            {
                entities.ForEach(entity =>
                {
                    resources.Add(entity.StartingResource);
                });

                writeCivCount = connection.Insert(entities);
                writeResCount = connection.Insert(resources);
                trans.Commit();
            }

            connection.Close();

            if (writeCivCount == writeResCount)
                return writeCivCount;
            return -1;
        }

        /// <inheritdoc />
        public void DropTables(IDbConnection connection)
        {
            connection.DropTableIfExists<CivilizationStartingResource>();
            connection.DropTableIfExists<Civilization>();
        }

        /// <inheritdoc />
        public void CreateTables(IDbConnection connection)
        {
            connection.CreateTable<Civilization>();
            connection.CreateTable<CivilizationStartingResource>();
        }
    }
}