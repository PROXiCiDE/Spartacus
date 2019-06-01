using System.ComponentModel.Composition;
using System.Diagnostics;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.AiCharacter
{
    [Export(typeof(ProtoUnitViewModel))]
    public class ProtoUnitViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {

        public void Handle(CharacterMessageQueue message)
        {
        }
    }
}