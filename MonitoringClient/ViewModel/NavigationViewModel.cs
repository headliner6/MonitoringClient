﻿using System;
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
            SelectedViewModel = new LogEntryViewModel(OpenLogMessageAddView, OpenLocationView);
        }
        private void OpenLogMessageAddView(object obj)
        {
            if (obj.ToString() == "LogMessageAddView")
            {
                var lvm = (IViewModel) selectedViewModel;
                SelectedViewModel = new LogMessageAddViewModel(OpenLogentryView);
                var lmavm = (LogMessageAddViewModel) selectedViewModel;
                lmavm.ConnectionString = lvm.ConnectionString;
            }
        }
        private void OpenLogentryView(object obj)
        {
            if (obj.ToString() == "LogEntryView")
            {
                var lmavm = (IViewModel) selectedViewModel;
                SelectedViewModel = new LogEntryViewModel(OpenLogMessageAddView, OpenLocationView);
                LogEntryViewModel lvm = (LogEntryViewModel) selectedViewModel;
                lvm.ConnectionString = lmavm.ConnectionString;
                lvm.LoadLogentries();
            }
        }
        private void OpenLocationView(object obj)
        {
            if (obj.ToString() == "LocationView")
            {
                var lvm = (IViewModel)selectedViewModel;
                SelectedViewModel = new LocationViewModel(OpenLogentryView);
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
