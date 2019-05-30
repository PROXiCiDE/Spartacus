using System.ComponentModel.Composition;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Quest
{
    [Export(typeof(PlayerSettingsViewModel))]
    public class PlayerSettingsViewModel : BasicViewModel
    {

        [ImportingConstructor]
        public PlayerSettingsViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
        }
    }
}