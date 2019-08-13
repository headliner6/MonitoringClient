using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvExporter
{
    public static class CsvCreator
    {
        private static string ToCsv(IEnumerable items)
        {
            var csvBuilder = new StringBuilder();
            var typeOfItems = items.GetType();
            var properties = typeOfItems.GetProperties();
            foreach (var item in items)
            {
                string line = string.Join(",", properties.Select(p => p.GetValue(item, null).ToCsvValue()).ToArray());
                csvBuilder.AppendLine(line);
            }
            return csvBuilder.ToString();
        }

        private static string ToCsvValue(this object item)
        {
            if (item == null) return "\"\"";

            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return string.Format("{0}", item);
            }
            return string.Format("\"{0}\"", item);
        }

        public static void Serialize(Stream stream, IEnumerable items)
        {
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(ToCsv(items).Trim());
            }
        }
    }

}
