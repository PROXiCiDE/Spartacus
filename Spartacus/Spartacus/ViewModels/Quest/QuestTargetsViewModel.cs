using System.ComponentModel.Composition;
using Spartacus.Common;

namespace Spartacus.ViewModels.Quest
{
    [Export(typeof(QuestTargetsViewModel))]
    public class QuestTargetsViewModel : BasicViewModel
    {
    }
}