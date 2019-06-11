using System.Collections.Generic;
using System.Data;
using SpartacusUtils.Bar;

namespace Spartacus.Logic.Builder
{
    public interface IModelBuilder<T>
    {
        List<T> FromBar(BarFileSystem barFileReader);
        List<T> FromRepository(IDbConnection connection);
    }
}