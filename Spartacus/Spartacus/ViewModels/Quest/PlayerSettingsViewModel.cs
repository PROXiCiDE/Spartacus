using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Quest
{
    public class PlayerSettingsViewModel : BasicViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        public PlayerSettingsViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
        }
    }
}