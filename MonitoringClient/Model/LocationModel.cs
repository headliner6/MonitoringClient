using LinqToDB.Mapping;
using MonitoringClient.Services;

namespace MonitoringClient.Model
{
    [Table ("location")]
    public partial class LocationModel : IModel
    {
        [Column ("LOCATION_ID"), PrimaryKey, NotNull]
        public int Id { get; set; }
        [Column ("PARENT_LOCATION")]
        public int ParentLocation { get; set; }
        [Column ("ADDRESS_FK")]
        public int Addressnumber { get; set; }
        [Column ("DESIGNATION")]
        public string Designation { get; set; }
        [Column ("BUILDING")]
        public int Building { get; set; }
        [Column ("ROOM")]
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
