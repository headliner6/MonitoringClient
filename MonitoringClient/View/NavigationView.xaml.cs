using System.Windows.Controls;
using MonitoringClient.ViewModel;

namespace MonitoringClient.View
{
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            InitializeComponent();
            this.DataContext = new NavigationViewModel();
        }
    }
}
