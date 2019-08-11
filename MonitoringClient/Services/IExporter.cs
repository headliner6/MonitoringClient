using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Services
{
    public interface IExporter
    {
        List<string> InitialiseExporters(string path);
        string ChooseExporterDllPath();
        string ChooseExportPath();
    }
}
