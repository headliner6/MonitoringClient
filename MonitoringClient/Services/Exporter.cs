using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Services
{
    public class Exporter : IExporter
    {
        public string ChooseExportPath()
        {
            string exportPath;
            using (var dialog = new CommonOpenFileDialog())
            {
                try
                {
                    dialog.ShowDialog();
                    exportPath = dialog.FileName;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return exportPath;
        }

        public string ChooseExporterDllPath()
        {
            string exporterDllPath;
            using (var dialog = new CommonOpenFileDialog())
            {
                try
                {
                    dialog.IsFolderPicker = true;
                    dialog.ShowDialog();
                    exporterDllPath = dialog.FileName;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return exporterDllPath;
        }

        public List<string> InitialiseExporters(string path)
        {
            var exporters = new List<string>();
            var loader = new PluginLoader();
            var exportersFromLoader = loader.GetDataExporters(path);
            foreach (var exporter in exportersFromLoader)
            {
                exporters.Add(exporter.Name);
            }
            return exporters;
        }
    }
}
