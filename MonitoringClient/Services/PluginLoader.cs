using PluginContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;

namespace MonitoringClient.Services
{
    public class PluginLoader
    {
        private readonly KeyValuePair<string, string> _defaultPath;

        public PluginLoader()
        {
            _defaultPath = new KeyValuePair<string, string>(@".\DataExporterDLL\", " *.dll");
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
        public ICollection<IDataExportPlugin> GetDataExporters(string path, string searchPattern)
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
            return Directory.GetFiles(_defaultPath.Key, _defaultPath.Value);
        }
        private string[] GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }
    }
}
