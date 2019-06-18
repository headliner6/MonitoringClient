using DuplicateCheckerLib;
using LinqToDB.Mapping;
using MonitoringClient.Services;
using System;

namespace MonitoringClient.Model
{
    [Table ("v_logentries")]
    public class LogEntryModel : IEntity, IModel
    {
        [Column ("id"), PrimaryKey, NotNull]
        public int Id { get; set; }
        [Column ("POD")]
        public string Pod { get; set; }
        [Column ("LOCATION")]
        public string Location { get; set; }
        [Column ("HOSTNAME")]
        public string Hostname { get; set; }
        [Column ("SEVERITY")]
        public int Severity { get; set; }
        [Column ("TIMESTAMP")]
        public DateTime Timestamp { get; set; }
        [Column ("MESSAGE")]
        public string Message { get; set; }

        public LogEntryModel() { }
        public LogEntryModel(int id, string pod, string location, string hostname, int severity, DateTime timestamp, string message)
        {
            this.Id = id;
            this.Pod = pod;
            this.Location = location;
            this.Hostname = hostname;
            this.Severity = severity;
            this.Timestamp = timestamp;
            this.Message = message;
        }

        public override bool Equals(object value)
        {
            return Equals(value as LogEntryModel);
        }

        public bool Equals(LogEntryModel other)
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

        public static bool operator ==(LogEntryModel lm1, LogEntryModel lm2)
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

        public static bool operator !=(LogEntryModel lm1, LogEntryModel lm2)
        {
            return !(lm1 == lm2);
        }

    }
}
