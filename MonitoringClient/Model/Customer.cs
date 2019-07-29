using MonitoringClient.Services;

namespace MonitoringClient.Model
{
    public class Customer : IModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Addressnumber { get; set; }
        public int CustomerAccountNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
    }
}
