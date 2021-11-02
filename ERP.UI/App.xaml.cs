using System.Reflection;
using System.Windows;
using ERP.Common.Resources;

namespace ERP.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string Version { get; private set; }

        public App() : base()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? Global.VersionNotFound;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, e.Exception.GetType().Name);
            e.Handled = true;
        }
    }
}
