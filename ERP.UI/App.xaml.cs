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
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string? Version { get; private set; }

        public App() : base()
        {
            logger.Info("Application starting up");

            SetCurrentAppVersion();
        }

        private void SetCurrentAppVersion()
        {
            logger.Debug("Setting up current app version");

            string? appVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();

            if (appVersion is null)
                logger.Warn("App version has not been found");
            else
                logger.Trace("App version = {version}", appVersion);

            Version = appVersion ?? Global.VersionNotFound;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            logger.Debug("Application Startup event called");
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            logger.Error(e.Exception, "Unhandled exception caught and handled by application's DispatcherUnhandledException");
            MessageBox.Show(e.Exception.Message, e.Exception.GetType().Name);
            e.Handled = true;
        }
    }
}
