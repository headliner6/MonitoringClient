using MonitoringClient.Services;

namespace MonitoringClient.Model
{
    public class LocationModel : IModel
    {
        public int Id { get; set; }
        public int ParentLocation { get; set; }
        public int Addressnumber { get; set; }
        public string Designation { get; set; }
        public int Building { get; set; }
        public int Room { get; set; }
        public LocationModel()
        { }
        public LocationModel(int id, int parentLocation, int addressnumber, string designation, int Building, int Room)
        {
            this.Id = id;
            this.ParentLocation = parentLocation;
            this.Addressnumber = addressnumber;
            this.Designation = designation;
            this.Building = Building;
            this.Room = Room;
        }
    }
}
