using System.ComponentModel.Composition;
using System.Diagnostics;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.AiCharacter
{
    [Export(typeof(ProtoUnitViewModel))]
    public class ProtoUnitViewModel : BasicViewModel, IHandle<CharacterMessageQueue>
    {

        [ImportingConstructor]
        public ProtoUnitViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
        }

        public void Handle(CharacterMessageQueue message)
        {
            Debug.WriteLine($"Handle {message.Filename}");
        }
    }
}