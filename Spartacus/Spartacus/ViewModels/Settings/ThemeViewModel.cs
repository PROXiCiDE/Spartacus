using System.ComponentModel.Composition;
using Spartacus.Common;

namespace Spartacus.ViewModels.Settings
{
    [Export(typeof(ThemeViewModel))]
    public class ThemeViewModel : BasicViewModel
    {
    }
}