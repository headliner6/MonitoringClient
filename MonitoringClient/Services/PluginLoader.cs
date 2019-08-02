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
        private readonly KeyValuePair<string, string> _defaultPath;

        //evtl. ist der Default-Path falsch sollte das Plugin nicht funktionieren.
        public PluginLoader()
        {
            _defaultPath = GetDefaultPath();
        }

        public List<IDataExportPlugin> GetDataExporters()
        {
            var dataexporters = new List<IDataExportPlugin>();
            var files = GetFiles();
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
        public List<IDataExportPlugin> GetDataExporters(string path, string searchPattern)
        {
            var dataexporters = new List<IDataExportPlugin>();
            var files = GetFiles(path, searchPattern);
            foreach (var file in files.Select(Path.GetFullPath))
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);
                    foreach (var typ in assembly.GetTypes().Where((typ) => typ == typeof(IDataExportPlugin) && typeof(IDataExportPlugin).IsAssignableFrom(typ)))
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

        private string[] GetFiles()
        {
            try
            {
                return Directory.GetFiles(_defaultPath.Key, _defaultPath.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Folgender Fehler ist aufgetreten: " + ex.Message);
                throw ex;
            }
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
        private KeyValuePair<string, string> GetDefaultPath()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var finalPath = Directory.GetParent(Directory.GetParent(currentPath).FullName).FullName + @"\DataExporterDLL";

            return new KeyValuePair<string, string>(finalPath, "*.dll");
        }
    }
}
