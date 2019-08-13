using Newtonsoft.Json;
using PluginContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextExporter
{
    public class JsonDataExporter : IDataExportPlugin
    {
        public string Name { get; set; }

        public JsonDataExporter()
        {
            Name = "JsonDataExporter";
        }

        public void Export(IEnumerable data, string destinationPath)
        {
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }
            var serializer = new JsonSerializer();
            var streamWriter = new StreamWriter(destinationPath);
            var writer = new JsonTextWriter(streamWriter);
            serializer.Serialize(writer, data);
            streamWriter.Close();
        }
    }
}
