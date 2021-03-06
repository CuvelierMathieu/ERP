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
    /// Logique d'interaction pour NavigationMenu.xaml
    /// </summary>
    public partial class NavigationMenu : UserControl
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public NavigationMenu()
        {
            logger.Debug("{WindowType} component constructed", nameof(NavigationMenu));
            InitializeComponent();
            logger.Trace("Component initialized");
        }
    }
}
