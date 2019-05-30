using Caliburn.Micro;

namespace Spartacus.ViewModels.Quest
{
    public class PlayerSettingsViewModel : Screen
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