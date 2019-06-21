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
    }
}
