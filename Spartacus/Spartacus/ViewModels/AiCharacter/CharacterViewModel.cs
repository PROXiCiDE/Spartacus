using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Caliburn.Micro;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Common;
using Spartacus.Common.MessageQueue;
using SpartacusUtils.ConfigManager;

namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {
        private AiCharacterXml _aiCharacter;

        [ImportingConstructor]
        public CharacterViewModel()
        {
        }

        public AiCharacterXml AiCharacter
        {
            get => _aiCharacter;
            set
            {
                _aiCharacter = value;
                NotifyOfPropertyChange(nameof(AiCharacter));
            }
        }

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