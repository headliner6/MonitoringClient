using MonitoringClient.Model;
using MonitoringClient.DataStructures;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Data;
using LinqToDB;

namespace MonitoringClient.Repository
{
    class LocationModelRepository : RepositoryBase<LocationModel>
    {
        public override string TableName { get; }
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
    }
}
