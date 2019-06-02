using System.ComponentModel.Composition;
using System.Diagnostics;
using Caliburn.Micro;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Common;
using Spartacus.Common.MessageQueue;

namespace Spartacus.ViewModels.AiCharacter
{
    [Export(typeof(DeveloperToolViewModel))]
    public class DeveloperToolViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {
        private AiCharacterXml _aiCharacter;

        [ImportingConstructor]
        public DeveloperToolViewModel()
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
            AiCharacter = message.AiCharacterXml;
        }
    }
}