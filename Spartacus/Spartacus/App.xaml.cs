using System.Configuration;
using System.Diagnostics;
using System.Windows;
using Spartacus.Common.ConfigManager;

namespace Spartacus
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ConfigInfo.LoadConfig();
            base.OnStartup(e);
        }
    }
}