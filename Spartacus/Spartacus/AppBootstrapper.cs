using Spartacus.ViewModels;
using System;
using System.Collections.Generic;
using Caliburn.Micro;
using SpartacusUtils.ConfigManager;

namespace Spartacus
{
    public class AppBootstrapper : BootstrapperBase
    {
        SimpleContainer container;
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.RegisterInstance(typeof(IConfigInfo), "IConfigInfo", new ConfigInfo());
            //container.Singleton<IConfigInfo, ConfigInfo>();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IShell, ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }
}