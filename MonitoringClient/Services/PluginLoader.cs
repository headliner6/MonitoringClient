using PluginContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Windows;

namespace MonitoringClient.Services
{
    public class PluginLoader
    {
        private string _searchPattern;

        //evtl. ist der Default-Path falsch sollte das Plugin nicht funktionieren.
        public PluginLoader()
        {
            _searchPattern = "*.dll";
        }

        public List<IDataExportPlugin> GetDataExporters(string path)
        {
            var dataexporters = new List<IDataExportPlugin>();
            var files = GetFiles(path, _searchPattern);
            foreach (var file in files.Select(Path.GetFullPath))
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);
                    foreach (var typ in assembly.GetTypes().Where((typ) => typ != typeof(IDataExportPlugin) && typeof(IDataExportPlugin).IsAssignableFrom(typ)))
                    {
                        var dataexporter = (IDataExportPlugin)Activator.CreateInstance(typ);
                        dataexporters.Add(dataexporter);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dataexporters;
        }

        private string[] GetFiles(string path, string searchPattern)
        {
            try
            {
                return Directory.GetFiles(path, searchPattern);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
        }
    }
}
