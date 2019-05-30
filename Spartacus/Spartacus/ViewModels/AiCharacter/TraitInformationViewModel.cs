using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.AiCharacter
{
    public class TraitInformationViewModel : BasicViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        public TraitInformationViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
        }
    }
}