using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Music
{
    public class MusicViewModel : BasicViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;

        public MusicViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
        }
    }
}