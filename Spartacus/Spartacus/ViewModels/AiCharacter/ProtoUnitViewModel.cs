using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.AiCharacter
{
    public class ProtoUnitViewModel : BasicViewModel
    {

        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        public ProtoUnitViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
        }
    }
}