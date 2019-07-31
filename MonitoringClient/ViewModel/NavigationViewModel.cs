using System.ComponentModel;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        private IViewModel selectedViewModel;
        public IViewModel SelectedViewModel
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
                var logEntryViewModel = selectedViewModel;
                SelectedViewModel = new LogMessageAddViewModel(OpenLogentryView);
                var logMessageAddViewModel = (LogMessageAddViewModel) selectedViewModel;
            }
        }
        private void OpenLocationView(object obj)
        {
            if (obj.ToString() == "LocationView")
            {
                var logEntryViewModel = selectedViewModel;
                SelectedViewModel = new LocationViewModel(OpenLogentryView);
                var locationViewModel = (LocationViewModel)selectedViewModel;
                locationViewModel.GetAll();
            }
        }

        private void OpenCustomerView(object obj)
        {
            if (obj.ToString() == "CustomerView")
            {
                var logEntryViewModel = selectedViewModel;
                SelectedViewModel = new CustomerViewModel(OpenLogentryView);
                var customerViewModel = (CustomerViewModel)selectedViewModel;
                customerViewModel.GetAll();
            }
        }

        private void OpenLogentryView(object obj)
        {
            if (obj.ToString() == "LogEntryView")
            {
                var logMessageAddViewModel = selectedViewModel;
                SelectedViewModel = new LogEntryViewModel(OpenLogMessageAddView, OpenLocationView, OpenCustomerView);
                LogEntryViewModel logEntryViewModel = (LogEntryViewModel) selectedViewModel;
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
