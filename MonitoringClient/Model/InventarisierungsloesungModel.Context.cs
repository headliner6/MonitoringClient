﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InventarisierungsloesungEntities : DbContext
    {
        public InventarisierungsloesungEntities()
            : base("name=InventarisierungsloesungEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accounting> Accounting { get; set; }
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Credentials> Credentials { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Deviceport> Deviceport { get; set; }
        public virtual DbSet<Devicetype> Devicetype { get; set; }
        public virtual DbSet<DevicetypeHasOperatingsystem> DevicetypeHasOperatingsystem { get; set; }
        public virtual DbSet<Interface> Interface { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<LogEntries> LogEntries { get; set; }
        public virtual DbSet<Network> Network { get; set; }
        public virtual DbSet<Operatingsystem> Operatingsystem { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Pointofdelivery> Pointofdelivery { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Productgroup> Productgroup { get; set; }
        public virtual DbSet<SoftwareService> SoftwareService { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<Transportmedium> Transportmedium { get; set; }
    
        [DbFunction("InventarisierungsloesungEntities", "GetLocationsHirarchicalRecursive")]
        public virtual IQueryable<GetLocationsHirarchicalRecursive_Result> GetLocationsHirarchicalRecursive()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetLocationsHirarchicalRecursive_Result>("[InventarisierungsloesungEntities].[GetLocationsHirarchicalRecursive]()");
        }
    
        public virtual int LogClear(Nullable<long> logEntriesId)
        {
            var logEntriesIdParameter = logEntriesId.HasValue ?
                new ObjectParameter("LogEntriesId", logEntriesId) :
                new ObjectParameter("LogEntriesId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LogClear", logEntriesIdParameter);
        }
    
        public virtual int LogMessageAdd(string i_pod, string i_hostname, Nullable<int> i_severity, string i_message)
        {
            var i_podParameter = i_pod != null ?
                new ObjectParameter("i_pod", i_pod) :
                new ObjectParameter("i_pod", typeof(string));
    
            var i_hostnameParameter = i_hostname != null ?
                new ObjectParameter("i_hostname", i_hostname) :
                new ObjectParameter("i_hostname", typeof(string));
    
            var i_severityParameter = i_severity.HasValue ?
                new ObjectParameter("i_severity", i_severity) :
                new ObjectParameter("i_severity", typeof(int));
    
            var i_messageParameter = i_message != null ?
                new ObjectParameter("i_message", i_message) :
                new ObjectParameter("i_message", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("LogMessageAdd", i_podParameter, i_hostnameParameter, i_severityParameter, i_messageParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
