using System.ComponentModel.Composition;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Quest
{
    [Export(typeof(QuestTargetsViewModel))]
    public class QuestTargetsViewModel : BasicViewModel
    {
        [ImportingConstructor]
        public QuestTargetsViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
        }
    }
}