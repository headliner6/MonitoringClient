using PluginContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvExporter
{
    public class CsvDataExporter : IDataExportPlugin
    {
        public string Name { get; set; }

        public CsvDataExporter()
        {
            Name = "CsvDataExporter";
        }

        public void Export(IEnumerable data, string destinationPath)
        {
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
            FileStream stream = File.Create(destinationPath);
        }
    }
}
