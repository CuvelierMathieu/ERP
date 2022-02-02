using System.Windows;

namespace ERP.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            logger.Debug("{WindowType} component constructed", nameof(MainWindow));
            InitializeComponent();
            logger.Trace("Component initialized");
        }
    }
}
