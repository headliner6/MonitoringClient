using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        public ICommand LoadLogentriesView { get; set; }
        public ICommand LoadLogMessageView { get; set; }
        private object selectedViewModel;
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }
        public NavigationViewModel()
        {
            SelectedViewModel = new LogentriesViewModel(OpenLogMessageAddView, OpenLocationsView);
        }
        private void OpenLogMessageAddView(object obj)
        {
            if (obj.ToString() == "LogMessageAddView")
            {
                var lvm = (IViewModel)selectedViewModel;
                SelectedViewModel = new LogMessageAddViewModel(OpenLogentriesView);
                var lmavm = (IViewModel) selectedViewModel;
                lmavm.ConnectionString = lvm.ConnectionString;
            }
        }
        private void OpenLogentriesView(object obj)
        {
            if (obj.ToString() == "LogentriesView")
            {
                var lmavm = (IViewModel)selectedViewModel;
                SelectedViewModel = new LogentriesViewModel(OpenLogMessageAddView, OpenLocationsView);
                LogentriesViewModel lvm = (LogentriesViewModel)selectedViewModel;
                lvm.ConnectionString = lmavm.ConnectionString;
                lvm.LoadLogentries();
            }
        }
        private void OpenLocationsView(object obj)
        {
            if (obj.ToString() == "LocationsView")
            {
                var lvm = (IViewModel)selectedViewModel;
                SelectedViewModel = new LocationsViewModel(OpenLogentriesView);
                var lcvm = (IViewModel)selectedViewModel;
                lcvm.ConnectionString = lvm.ConnectionString;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
