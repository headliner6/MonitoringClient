using LinqToDB.Mapping;
using MonitoringClient.Services;
using System;

namespace MonitoringClient.Model
{
    [Table ("customer"), Serializable]
    public class CustomerModel : IModel
    {
        [Column("CUSTOMER_ID"), PrimaryKey, NotNull]
        public int Id { get; set; }
        [Column ("FIRSTNAME")]
        public string Firstname { get; set; }
        [Column("LASTNAME")]
        public string Lastname { get; set; }
        [Column("ADDRESSNUMBER")]
        public string Addressnumber { get; set; }
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
