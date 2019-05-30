using System.ComponentModel.Composition;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Music
{
    [Export(typeof(MusicViewModel))]
    public class MusicViewModel : BasicViewModel
    {
        [ImportingConstructor]
        public MusicViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
        }
    }
}