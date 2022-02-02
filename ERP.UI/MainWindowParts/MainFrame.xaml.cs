using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERP.UI.MainWindowParts
{
    /// <summary>
    /// Logique d'interaction pour MainFrame.xaml
    /// </summary>
    public partial class MainFrame : UserControl
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MainFrame()
        {
            logger.Debug("{WindowType} component constructed", nameof(MainFrame));
            InitializeComponent();
            logger.Trace("Component initialized");
        }
    }
}
