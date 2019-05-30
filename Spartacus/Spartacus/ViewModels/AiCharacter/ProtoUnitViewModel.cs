using Caliburn.Micro;

namespace Spartacus.ViewModels.AiCharacter
{
    public class ProtoUnitViewModel : Screen
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