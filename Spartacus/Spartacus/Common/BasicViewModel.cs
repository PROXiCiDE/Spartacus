using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using SpartacusUtils.ConfigManager;

namespace Spartacus.Common
{
    public class BasicViewModel : Screen, IWikiDocumentation
    {
        internal readonly IWindowManager _windowManager;
        internal readonly IEventAggregator _eventAggregator;
        internal readonly IConfigInfo _configInfo;

        public BasicViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
        }

        protected BasicViewModel()
        {
            _eventAggregator = IoC.Get<IEventAggregator>();
            _windowManager = IoC.Get<IWindowManager>();
            _configInfo = IoC.Get<IConfigInfo>();
        }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        public string WikiUrl { get; set; } = "https://github.com/PROXiCiDE/Spartacus/wiki";
    }
}
