//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MonitoringClient.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Address()
        {
            this.Location = new HashSet<Location>();
        }
    
        public long Id { get; set; }
        public long Town_FK { get; set; }
        public string Streetname { get; set; }
        public string Streetnumber { get; set; }
        public string Country { get; set; }
        public string Additive { get; set; }
        public Nullable<long> Pobox { get; set; }
    
        public virtual Town Town { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Location> Location { get; set; }
    }
}
