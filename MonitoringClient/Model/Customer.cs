using MonitoringClient.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonitoringClient.Model
{
    public class Customer : IModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Addressnumber { get; set; }
        [Required]
        public int CustomerAccountNumber { get; set; }
        [ForeignKey("CustomerAccountNumber")]
        public virtual Kundenkonto CustomerAccount { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Website { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
