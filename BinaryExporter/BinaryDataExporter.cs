using PluginContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BinaryExporter
{
    public class BinaryDataExporter : IDataExportPlugin
    {
        public string Name { get; set; }

        public BinaryDataExporter()
        {
            Name = "BinaryDataExporter";
        }

        public void Export(IEnumerable data, string destinationPath)
        {
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
            FileStream stream = File.Create(destinationPath);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
}
