using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Spartacus.Common;

namespace Spartacus.ViewModels.Settings
{
    [Export(typeof(ThemeViewModel))]
    public class ThemeViewModel : BasicViewModel
    {
        [ImportingConstructor]
        public ThemeViewModel(IWindowManager windowManager, IEventAggregator eventAggregator) : base(windowManager, eventAggregator)
        {
        }
    }
}
