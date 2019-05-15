using DuplicateCheckerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Model
{
    public class LogentriesModel : IEntity
    {
        public int Id { get; set; }
        public string Pod { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public int Severity { get; set; }
        public string Timestamp { get; set; }
        public string Message { get; set; }

        public LogentriesModel(int id, string pod, string location, string hostname, int severity, string timestamp, string message)
        {
            this.Id = id;
            this.Pod = pod;
            this.Location = location;
            this.Hostname = hostname;
            this.Severity = severity;
            this.Timestamp = timestamp;
            this.Message = message;
        }

        public bool Equals(LogentriesModel other)
        {
            if (object.ReferenceEquals(null, other)) return false;
            if (object.ReferenceEquals(this, other)) return true;

            return (object.Equals(this.Severity, other.Severity)) &&
                   (object.Equals(this.Message, other.Message));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.Severity) ? this.Severity.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!object.ReferenceEquals(null, this.Message) ? this.Message.GetHashCode() : 0);
                return hash;
            }
        }

        public static bool operator ==(LogentriesModel lm1, LogentriesModel lm2)
        {
            if (object.ReferenceEquals(lm1, lm2))
            {
                return true;
            }
            if (object.ReferenceEquals(null, lm1))
            {
                return false;
            }
            return (lm1.Equals(lm2));
        }
        public static bool operator !=(LogentriesModel lm1, LogentriesModel lm2)
        {
            return !(lm1 == lm2);
        }

    }
}
