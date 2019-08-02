using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MonitoringClient.RegExp
{
    public class ExportValidation
    {
        public bool ExportPathValidation(string exportPath)
        {
            if (exportPath == null || !Regex.IsMatch(exportPath, @"^(?!\s*$).+"))
            {
                MessageBox.Show("Exportpfad wurde nicht ausgewählt!");
                return false;
            }
            return true;
        }

        public bool ExportDllPathValidation(string exportDllPath)
        {
            if (exportDllPath == null || !Regex.IsMatch(exportDllPath, @"^(?!\s*$).+"))
            {
                MessageBox.Show("Es wurde kein [Exporter].dll File ausgewählt!");
                return false;
            }
            return true;
        }

        public bool SelectedExporterValidation(string selectedExporter)
        {
            if (selectedExporter == null || !Regex.IsMatch(selectedExporter, @"^(?!\s*$).+"))
            {
                MessageBox.Show("Es wurde kein [Exporter].dll File gefunden, welches den Exportrichtlinien entsprechen!");
                return false;
            }
            return true;
        }
    }
}
