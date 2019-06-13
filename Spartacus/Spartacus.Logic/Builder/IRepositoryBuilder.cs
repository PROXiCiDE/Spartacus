using System.Collections.Generic;
using System.Data;
using SpartacusUtils.Bar;

namespace Spartacus.Logic.Builder
{
    public interface IRepositoryBuilder<TEntity>
    {
        /// <summary>
        /// Reads the entry from a bar file into a database module
        /// </summary>
        /// <param name="barFile"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FromBar(BarFileSystem barFile);

        /// <summary>
        /// Gets the database data from a table
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FromRepository(IDbConnection connection);

        /// <summary>
        /// Inserts / Updates data from a table
        /// </summary>
        /// <param name="connection"></param>
        long InsertRepository(IDbConnection connection, IEnumerable<TEntity> entities);

        /// <summary>
        /// Drop tables that are associated with this repository
        /// </summary>
        /// <param name="connection"></param>
        void DropTables(IDbConnection connection);

        /// <summary>
        /// Create tables that are associated with this repository
        /// </summary>
        /// <param name="connection"></param>
        void CreateTables(IDbConnection connection);
    }
}