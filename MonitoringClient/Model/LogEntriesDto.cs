using DuplicateCheckerLib;
using MonitoringClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Model
{
    public class LogEntriesDto : LogEntries, IModel, IEntity
    {
        public override bool Equals(object value)
        {
            return Equals(value as LogEntries);
        }

        public bool Equals(LogEntries other)
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

        public static bool operator ==(LogEntriesDto lm1, LogEntriesDto lm2)
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

        public static bool operator !=(LogEntriesDto lm1, LogEntriesDto lm2)
        {
            return !(lm1 == lm2);
        }
    }
}
