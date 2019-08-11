﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.ViewModel
{
    public interface IViewModel
    {
        string ConnectionString { get; set; }
        void GetAll();
        void Export();
        void ChooseExportPath();
        void ChooseExporterDllPath();
    }
}
