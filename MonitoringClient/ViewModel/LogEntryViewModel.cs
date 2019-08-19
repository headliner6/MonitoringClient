using DuplicateCheckerLib;
using Microsoft.WindowsAPICodePack.Dialogs;
using MonitoringClient.Command;
using MonitoringClient.IoC;
using MonitoringClient.Model;
using MonitoringClient.RegExp;
using MonitoringClient.Repository;
using MonitoringClient.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MonitoringClient.ViewModel
{
    public class LogEntryViewModel : INotifyPropertyChanged, IViewModel
    {
        private readonly Action<object> navigateToLogMessageAddView;
        private readonly Action<object> navigateToLocationView;
        private readonly Action<object> navigateToCustomerView;
        private readonly ILogEntryRepository _logEntryModelRepository;
        private DuplicateChecker _duplicateChecker;
        private ObservableCollection<LogEntryModel> _logentries;
        private string _selectedExporter;
        private string _exportPath;
        private string _exporterDllPath;
        private IExporter _exporter;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand NavigateLogMessageAddView { get; set; }
        public ICommand NavigateLocationView { get; set; }
        public ICommand NavigateCustomerView { get; set; }
        public ObservableCollection<LogEntryModel> Logentries
        {
            get { return _logentries; }
            set
            {
                _logentries = value;
                OnPropertyChanged("Logentries");
            }
        }
        public string ConnectionString { get; set; }
        public GetAllCommand LoadButtonCommand { get; set; }
        public ConfirmLogEntriesButtonCommand ConfirmButtonCommand { get; set; }
        public FindDuplicatesButtonCommand FindDuplicatesButtonCommand { get; set; }
        public ExportDataCommand ExportLogentries { get; set; }
        public List<string> Exporters { get; set; }
        public string SelectedExporter
        {
            get
            {
                return _selectedExporter;
            }
            set
            {
                _selectedExporter = value;
                OnPropertyChanged("SelectedExporter");
            }
        }
        public string ExportPath
        {
            get
            {
                return _exportPath;
            }
            set
            {
                _exportPath = value;
                OnPropertyChanged("ExportPath");
            }
        }

        public LogEntryViewModel(Action<object> navigateToLogMessageAddView, Action<object> navigateToLocationView, Action<object> navigateToCustomerView)
        {
            NavigateLogMessageAddView = new BaseCommand(StartLogMessageAddView);
            NavigateLocationView = new BaseCommand(StartLocationView);
            NavigateCustomerView = new BaseCommand(StartCustomerView);
            LoadButtonCommand = new GetAllCommand(this);
            ConfirmButtonCommand = new ConfirmLogEntriesButtonCommand(this);
            FindDuplicatesButtonCommand = new FindDuplicatesButtonCommand(this);
            ExportLogentries = new ExportDataCommand(this);
            _exporter = new Exporter();
            this.navigateToLogMessageAddView = navigateToLogMessageAddView;
            this.navigateToLocationView = navigateToLocationView;
            this.navigateToCustomerView = navigateToCustomerView;

            Logentries = new ObservableCollection<LogEntryModel>();
            _duplicateChecker = new DuplicateChecker();
            _logEntryModelRepository = GetLogEntryModelRepository();

            ConnectionString = _logEntryModelRepository.ConnectionString;
        }
        public void GetAll()
        {

            try
            {
                _logEntryModelRepository.ConnectionString = ConnectionString;
                Logentries = new ObservableCollection<LogEntryModel>(_logEntryModelRepository.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
            }
        }
        public void ConfirmLogentries(int id)
        {
            _logEntryModelRepository.ConfirmLogentries(id);
        }
        public void CheckForDuplicates()
        {
            var logentries = new ObservableCollection<LogEntryModel>(_duplicateChecker.FindDuplicates(Logentries).Cast<LogEntryModel>());
            if (logentries.Count == 0)
            {
                MessageBox.Show("Keine Duplikate vorhanden!");
            }
            else
            {
                Logentries = logentries;
            }
        }
        public void Export()
        {
            if (Logentries.Count != 0)
            {
                var validator = new ExportValidation();
                if (validator.ExportPathValidation(_exportPath))
                {
                    if (validator.ExportDllPathValidation(_exporterDllPath))
                    {
                        if (validator.SelectedExporterValidation(_selectedExporter))
                        {
                            var loader = new PluginLoader();
                            try
                            {
                                var exporters = loader.GetDataExporters(_exporterDllPath);
                                foreach (var exporter in exporters)
                                {
                                    if (exporter.Name == SelectedExporter)
                                    {
                                        exporter.Export(Logentries, _exportPath);
                                        MessageBox.Show("Export erfolgreich! --> Path: " + _exportPath);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Keine Logentries verfügbar!");
            }
        }
        public void ChooseExportPath()
        {
            try
            {
                ExportPath = _exporter.ChooseExportPath();
            }
            catch (Exception ex) { }
        }

        public void ChooseExporterDllPath()
        {
            try
            {
                _exporterDllPath = _exporter.ChooseExporterDllPath();
                Exporters = _exporter.InitialiseExporters(_exporterDllPath);
                SelectedExporter = Exporters.FirstOrDefault();
                OnPropertyChanged("Exporters");
            }
            catch (Exception ex) { }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ILogEntryRepository GetLogEntryModelRepository()
        {
            var injector = new Injector();
            return injector.InjectLogEntryRepository();
        }
        private void StartLogMessageAddView(object obj)
        {
            navigateToLogMessageAddView.Invoke("LogMessageAddView");
        }
        private void StartLocationView(object obj)
        {
            navigateToLocationView.Invoke("LocationView");
        }
        private void StartCustomerView(object obj)
        {
            navigateToCustomerView.Invoke("CustomerView");
        }
    }
}
