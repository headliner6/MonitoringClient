//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonitoringClient.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pointofdelivery
    {
        public long Id { get; set; }
        public long Customer_id_fk { get; set; }
        public long Contact_id_fk { get; set; }
        public long Cocation_fk { get; set; }
        public string Designation { get; set; }
        public System.TimeSpan Timezone { get; set; }
        public short TimeZonePositiv { get; set; }
        public string NtpServerIp { get; set; }
    
        public virtual Contact Contact { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
    }
}
