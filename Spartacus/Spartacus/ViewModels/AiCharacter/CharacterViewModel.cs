using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Documents;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {
        private readonly ObservableCollection<string> _characterCivs;

        public ObservableCollection<string> CharacterCivs
        {
            get { return _characterCivs; }
        }

        [ImportingConstructor]
        public CharacterViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
            _characterCivs = new ObservableCollection<string>()
            {
                "Greek",
                "Egyptian"
            };

            Debug.WriteLine("CharacterViewModel CTOR");
        }

        public void Handle(CharacterMessageQueue message)
        {
           Debug.WriteLine(message.Filename);
        }
    }
}