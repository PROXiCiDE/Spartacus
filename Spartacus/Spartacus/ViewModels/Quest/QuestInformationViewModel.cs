using System.ComponentModel.Composition;
using Spartacus.Common;

namespace Spartacus.ViewModels.Quest
{
    [Export(typeof(QuestInformationViewModel))]
    public class QuestInformationViewModel : BasicViewModel
    {
    }
}