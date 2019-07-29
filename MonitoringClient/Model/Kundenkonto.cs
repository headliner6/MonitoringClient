using MonitoringClient.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringClient.Model
{
    public class Kundenkonto : IModel
    {
        public int Id { get; set; }

        [Key]
        public int Kundenkonto_id { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

    }
}
