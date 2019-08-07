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
    using MonitoringClient.Services;
    using System;
    using System.Collections.Generic;
    
    public partial class Location : IModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            this.Accounting = new HashSet<Accounting>();
            this.Device = new HashSet<Device>();
            this.Pointofdelivery = new HashSet<Pointofdelivery>();
        }
    
        public long Id { get; set; }
        public Nullable<long> ParentLocation { get; set; }
        public Nullable<long> Addressnumber { get; set; }
        public string Designation { get; set; }
        public Nullable<long> Building { get; set; }
        public Nullable<long> Room { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accounting> Accounting { get; set; }
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Device> Device { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pointofdelivery> Pointofdelivery { get; set; }
    }
}
