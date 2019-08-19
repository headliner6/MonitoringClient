using MonitoringClient.Model;
using MonitoringClient.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Services
{
    public interface ILogEntryRepository : IRepositoryBase<LogEntryModel>
    {
        void ConfirmLogentries(int id);
        void AddMessage(string pod, string hostname, string severity, string message);
    }
}
