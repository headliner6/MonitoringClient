using PluginContracts;
using System;
using System.Collections;
using System.Reflection;
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
            var csvString = CreateCsvString<IEnumerable>(data);
            File.WriteAllText(destinationPath, csvString);
        }

        private string CreateCsvString<T>(IEnumerable data) where T : class
        {
            var csvString = "";
            var delimiter = ";";
            var properties = data.GetType().GetGenericArguments().FirstOrDefault().GetProperties();

            using (var stringWriter = new StringWriter())
            {
                var headerRow = properties
                .Select(n => n.Name)
                .Aggregate((columnA, colmnB) => columnA + delimiter + colmnB);

                stringWriter.WriteLine(headerRow);

                foreach (var element in data)
                {
                    var positionRow = properties
                    .Select(n => n.GetValue(element, null))
                    .Select(n => n == null ? "null" : n.ToString())
                    .Aggregate((a, b) => a + delimiter + b);

                    stringWriter.WriteLine(positionRow);
                }
                csvString = stringWriter.ToString();
            }
            return csvString;
        }
    }
}
