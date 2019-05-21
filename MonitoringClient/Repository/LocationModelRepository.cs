using MonitoringClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Repository
{
    class LocationModelRepository : RepositoryBase<LocationModel>
    {
        public override string TableName { get; }
        public override ObservableCollection<LocationModel> Items { get; set; }

        public LocationModelRepository()
        {
            Items = new ObservableCollection<LocationModel>();
            TableName = "Location";
        }

        public override LocationModel GetSingle<P>(P pkValue)
        {
            throw new NotImplementedException();
        }

        public override void Add(LocationModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(LocationModel entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(LocationModel entity)
        {
            throw new NotImplementedException();
        }

        public override List<LocationModel> GetAll(string whereCondition, Dictionary<string, object> parameterValues)
        {
            throw new NotImplementedException();
        }

        public override List<LocationModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
