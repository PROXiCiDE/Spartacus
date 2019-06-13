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
        IEnumerable<TEntity> FromXml(BarFileSystem barFile, IDbConnection connection);

        IEnumerable<TEntity> FromRepository(IDbConnection connection);
    }
}