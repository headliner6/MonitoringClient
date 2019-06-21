using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Model
{
    [Table ("customer")]
    public class CustomerModel
    {
        [Column("CUSTOMER_ID")]
        public int CustomerId { get; set; }
        [Column ("FIRSTNAME")]
        public string Firstname { get; set; }
        [Column("LASTNAME")]
        public string Lastname { get; set; }
        [Column("ADDRESSNUMBER")]
        public int Addressnumber { get; set; }
        [Column("KUNDENKONTO_FK")]
        public int CustomerAccountNumber { get; set; }
        [Column("TEL")]
        public string PhoneNumber { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("URL")]
        public string Website { get; set; }
        [Column("PASSWORD")]
        public string Password { get; set; }
    }
}
