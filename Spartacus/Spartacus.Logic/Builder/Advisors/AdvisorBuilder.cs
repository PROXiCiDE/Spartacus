using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using Dapper.Contrib.Extensions;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Database.DBModels.Advisors;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.SQLite;

namespace Spartacus.Logic.Builder.Advisors
{
    public class AdvisorBuilder : IRepositoryBuilder<Advisor>
    {
        public IEnumerable<Advisor> FromBar(BarFileSystem barFile)
        {
            var advisors = new List<Advisor>();

            var entry = barFile.GetEntry(StringResource.XmlFile_Advisors);
            if (entry != null)
            {
                var xmlClass = barFile.ReadEntry<EconAdvisorsXml>(entry);

                xmlClass?.AdvisorArray.ForEach(advisor =>
                {
                    advisors.Add(new Advisor(advisor));
                });
            }

            return advisors;
        }

        public IEnumerable<Advisor> FromRepository(IDbConnection connection)
        {
            return connection.GetAll<Advisor>();
        }

        public long InsertRepository(IDbConnection connection, IEnumerable<Advisor> entities)
        {
            long written = 0;
            connection.Open();
            using (var trans = connection.BeginTransaction())
            {
                written = connection.Insert(entities);
                trans.Commit();
            }
            connection.Close();
            return written;
        }

        public void DropTables(IDbConnection connection)
        {
            connection.DropTableIfExists<Advisor>();
        }

        public void CreateTables(IDbConnection connection)
        {
            connection.CreateTable<Advisor>();
        }
       
    }
}