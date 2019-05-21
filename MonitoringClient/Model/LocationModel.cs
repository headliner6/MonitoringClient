using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Model
{
    public class LocationModel
    {
        public int Id { get; set; }
        public int AdressNumber { get; set; }
        public string Designation { get; set; }
        public int Building { get; set; }
        public int Room { get; set; }
        public LocationModel(int id, int adressnumber, string designation, int Building, int Room)
        {
            this.Id = id;
            this.AdressNumber = adressnumber;
            this.Designation = designation;
            this.Building = Building;
            this.Room = Room;
        }
    }
}
