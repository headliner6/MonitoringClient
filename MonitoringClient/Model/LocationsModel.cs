
namespace MonitoringClient.Model
{
    public class LocationsModel
    {
        public int Id { get; set; }
        public int Addressnumber { get; set; }
        public string Designation { get; set; }
        public int Building { get; set; }
        public int Room { get; set; }
        public LocationsModel(int id, int addressnumber, string designation, int Building, int Room)
        {
            this.Id = id;
            this.Addressnumber = addressnumber;
            this.Designation = designation;
            this.Building = Building;
            this.Room = Room;
        }
    }
}
