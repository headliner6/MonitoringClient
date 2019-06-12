using MonitoringClient.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MonitoringClient.DataStructures
{
    public class LocationTreeBuilder
    {
        public ObservableCollection<LocationNode> BuildTree(List<LocationModel> locations)
        {
            var mainParentNode = FindMainTreeRootNode(locations);
            locations.Reverse();
            BuildTree(mainParentNode, locations);
            var tree = new ObservableCollection<LocationNode>();
            tree.Add(mainParentNode);
            return tree;
        }

        private void BuildTree(LocationNode mainParentNode, List<LocationModel> locations)
        {
            foreach (var location in locations.Reverse<LocationModel>())
            {
                if (location.ParentLocation.Equals(mainParentNode.Location.Id))
                {
                    mainParentNode.ChildNodes.Add(new LocationNode(location));
                    locations.Remove(location);
                }
                else
                {
                    foreach (var child in mainParentNode.ChildNodes)
                    BuildTree(child, locations);
                }
            }
        }

        private LocationNode FindMainTreeRootNode(List<LocationModel> locations)
        {
            LocationNode mainParentNode = null;
            foreach (var location in locations.Reverse<LocationModel>())
            {
                if (location.ParentLocation == 0)
                {
                    mainParentNode = new LocationNode(location);
                    locations.Remove(location);
                }
            }
            return mainParentNode;
        }
    }
}
