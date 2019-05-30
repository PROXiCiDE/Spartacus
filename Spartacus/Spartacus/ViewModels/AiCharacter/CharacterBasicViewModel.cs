using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterBasicViewModel : BasicViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ObservableCollection<string> _characterCivs;

        public ObservableCollection<string> CharacterCivs
        {
            get { return _characterCivs; }
        }

        public CharacterBasicViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
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