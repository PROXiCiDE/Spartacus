using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using ProjectCeleste.GameFiles.XMLParser;
using Spartacus.Common;
using Spartacus.Common.MessageQueue;
using SpartacusUtils.Helpers;

namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {
        private AiCharacterXml _aiCharacter;
        private ObservableCollection<string> _protoUnits;

        public ObservableCollection<string> ProtoUnits
        {
            get => _protoUnits;
            set
            {
                if (Equals(value, _protoUnits)) return;
                _protoUnits = value;
                NotifyOfPropertyChange();
            }
        }

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

            ProtoUnits = new ObservableCollection<string>();
            AiCharacter?.ProtoUnits?.ProtoUnit.ForEach(unit =>
            {
                ProtoUnits.Add(unit);
            });
        }

        //TODO: CHAR_PROTO_UNITS Get information from database
        //ListView -> Icon, Name, Civ Name
        public void LoadProtoUnitInformation(AiCharacterXml aiCharacterXml)
        {

        }
    }
}