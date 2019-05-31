using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Caliburn.Micro;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Common;
using Spartacus.Common.ConfigManager;

namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {
        private AiCharacterXml _aiCharacter;

        [ImportingConstructor]
        public CharacterViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager,
            eventAggregator)
        {
            ConfigInfo.CivilizationList.ForEach(x => CharacterCivs.Add(new CivilizationData(x.CivId, x.Name)));
        }

        public AiCharacterXml AiCharacter
        {
            get => _aiCharacter;
            set
            {
                _aiCharacter = value;
                NotifyOfPropertyChange(() => AiCharacter);
            }
        }

        public ObservableCollection<CivilizationData> CharacterCivs { get; } =
            new ObservableCollection<CivilizationData>();

        public void Handle(CharacterMessageQueue message)
        {
            SetAiCharacter(message.AiCharacterXml);
            //_eventAggregator.PublishOnUIThread(new MessageQueue(message.Filename));
        }

        public void SetAiCharacter(AiCharacterXml aiCharacterXml)
        {
            AiCharacter = aiCharacterXml;
        }
    }
}