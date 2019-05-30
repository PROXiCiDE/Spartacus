using System.ComponentModel.Composition;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Quest
{
    [Export(typeof(ObjectivesViewModel))]
    public class ObjectivesViewModel : BasicViewModel
    {

        [ImportingConstructor]
        public ObjectivesViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
        }
    }
}