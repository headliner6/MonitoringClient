using MonitoringClient.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Data;
using LinqToDB;
=======

>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f

namespace MonitoringClient.Repository
{
    class LocationModelRepository : RepositoryBase<LocationModel>
    {
        public override string TableName { get; }
<<<<<<< HEAD
=======
        public override string PrimaryKey { get { return "location_id"; } }
        public override string InsertIntoEntityFieldForSqlStatement { get { return "parent_location, address_fk, designation, building, room"; } }
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
        public LocationModelRepository()
        {
            TableName = "location";
        }

        public override List<LocationModel> GetEntitiesFromDB(MySqlDataReader reader)
        {
            var locations = new List<LocationModel>();
            while (reader.Read())
            {
                locations.Add(new LocationModel(
                    reader.GetInt32("location_id"),
                    reader.GetInt32("parent_location"),
                    reader.GetInt32("address_fk"),
                    reader.GetValue(reader.GetOrdinal("designation")) as string,
                    reader.GetInt32("building"),
                    reader.GetInt32("room")
                ));
            }
            return locations;
        }
<<<<<<< HEAD
=======

        public override LocationModel GetEntityFromDB(MySqlDataReader reader)
        {
           var location = new LocationModel();
            while (reader.Read())
            {
                location = new LocationModel(
                    reader.GetInt32("location_id"),
                    reader.GetInt32("parent_location"),
                    reader.GetInt32("address_fk"),
                    reader.GetValue(reader.GetOrdinal("designation")) as string,
                    reader.GetInt32("building"),
                    reader.GetInt32("room")
                );
            }
            return location;
        }

        public override string UpdateSqlStatementValues(LocationModel entity)
        {
            return $"parent_location = {entity.ParentLocation}, address_fk = {entity.Addressnumber}, designation = '{entity.Designation}', building = {entity.Building}, room = {entity.Room}";
        }

        public override string AddSqlStatementValues(LocationModel entity)
        {
            return $"{entity.ParentLocation},{entity.Addressnumber},'{entity.Designation}', {entity.Building}, {entity.Room}";
        }
>>>>>>> 1b75c64bf9613056678978c0e6f12a0a5832905f
    }
}
