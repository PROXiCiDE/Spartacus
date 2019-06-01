using System.ComponentModel.Composition;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.AiCharacter
{
    [Export(typeof(TraitInformationViewModel))]
    public class TraitInformationViewModel : BasicViewModel
    {
    }
}