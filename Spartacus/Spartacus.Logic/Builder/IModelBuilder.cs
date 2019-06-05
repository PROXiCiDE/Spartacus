using System.Collections.Generic;
using SpartacusUtils.Bar;

namespace Spartacus.Logic.Builder
{
    public interface IModelBuilder<T, in TRepository>
    {
        List<T> FromBar(BarFileSystem barFileReader);
        List<T> FromRepository(TRepository repository);
    }
}