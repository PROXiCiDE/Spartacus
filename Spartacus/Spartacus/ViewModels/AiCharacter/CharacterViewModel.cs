using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using Caliburn.Micro;

namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<string> _characterCivs;

        public ObservableCollection<string> CharacterCivs
        {
            get { return _characterCivs; }
        }

        public CharacterViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;

            _characterCivs = new ObservableCollection<string>()
            {
                "Greek",
                "Egyptian"
            };
        }
    }
}