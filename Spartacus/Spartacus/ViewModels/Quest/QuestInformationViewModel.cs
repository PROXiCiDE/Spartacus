using System.ComponentModel.Composition;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Quest
{
    [Export(typeof(QuestInformationViewModel))]
    public class QuestInformationViewModel : BasicViewModel
    {

        [ImportingConstructor]
        public QuestInformationViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
        }
    }
}