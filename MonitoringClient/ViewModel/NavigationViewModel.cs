using System.ComponentModel;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
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
            SelectedViewModel = new LogEntryViewModel(OpenLogMessageAddView, OpenLocationView, OpenCustomerView);
        }
        private void OpenLogMessageAddView(object obj)
        {
            if (obj.ToString() == "LogMessageAddView")
            {
                var logEntryViewModel = (IViewModel) selectedViewModel;
                SelectedViewModel = new LogMessageAddViewModel(OpenLogentryView);
                var logMessageAddViewModel = (LogMessageAddViewModel) selectedViewModel;
                logMessageAddViewModel.ConnectionString = logEntryViewModel.ConnectionString;
            }
        }
        private void OpenLocationView(object obj)
        {
            if (obj.ToString() == "LocationView")
            {
                var logEntryViewModel = (IViewModel)selectedViewModel;
                SelectedViewModel = new LocationViewModel(OpenLogentryView);
                var locationViewModel = (LocationViewModel)selectedViewModel;
                locationViewModel.ConnectionString = logEntryViewModel.ConnectionString;
                locationViewModel.GetAll();
            }
        }

        private void OpenCustomerView(object obj)
        {
            if (obj.ToString() == "CustomerView")
            {
                var logEntryViewModel = (IViewModel)selectedViewModel;
                SelectedViewModel = new CustomerViewModel(OpenLogentryView);
                var customerViewModel = (CustomerViewModel)selectedViewModel;
                customerViewModel.ConnectionString = logEntryViewModel.ConnectionString;
                customerViewModel.GetAll();
            }
        }

        private void OpenLogentryView(object obj)
        {
            if (obj.ToString() == "LogEntryView")
            {
                var logMessageAddViewModel = (IViewModel) selectedViewModel;
                SelectedViewModel = new LogEntryViewModel(OpenLogMessageAddView, OpenLocationView, OpenCustomerView);
                LogEntryViewModel logEntryViewModel = (LogEntryViewModel) selectedViewModel;
                logEntryViewModel.ConnectionString = logMessageAddViewModel.ConnectionString;
                logEntryViewModel.GetAll();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
