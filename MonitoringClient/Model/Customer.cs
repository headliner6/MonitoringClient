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
    
    public partial class Customer : IModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Pointofdelivery = new HashSet<Pointofdelivery>();
        }
    
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Addressnumber { get; set; }
        public long CustomerAccountNumber { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
    
        public virtual CustomerAccount CustomerAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pointofdelivery> Pointofdelivery { get; set; }
    }
}
