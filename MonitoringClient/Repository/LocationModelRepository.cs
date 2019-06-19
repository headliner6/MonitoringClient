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
        public override string PrimaryKey { get { return "location_id"; } }
        public override string InsertIntoEntityFieldForSqlStatement { get { return "parent_location, address_fk, designation, building, room"; } }
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

        public void Test(DataContext context)
        {
            var locations = context.GetTable<LocationModel>();
            IQueryable<LocationModel> locationQuery = locations.Where(l => l.Id.Equals(PrimaryKey));
        }
    }
}
