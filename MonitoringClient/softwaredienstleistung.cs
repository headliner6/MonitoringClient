//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonitoringClient
{
    using System;
    using System.Collections.Generic;
    
    public partial class softwaredienstleistung
    {
        public int software_id { get; set; }
        public int stundenaufwand { get; set; }
        public long abrechung_fk { get; set; }
    
        public virtual abrechnung abrechnung { get; set; }
    }
}
