using MonitoringClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.DataStructures
{
    public class LocationTreeBuilder
    {
        public List<TreeNode<LocationModel>> BuildTree(List<LocationModel> locations)
        {
            var mainParentNode = FindMainTreeRootNode(locations);
            locations.Reverse();
            BuildTree(mainParentNode, locations);
            var tree = new List<TreeNode<LocationModel>>();
            tree.Add(mainParentNode);
            return tree;
        }

        private void BuildTree(TreeNode<LocationModel> mainParentNode, List<LocationModel> locations)
        {
            foreach (var location in locations.Reverse<LocationModel>())
            {
                if (location.ParentLocation.Equals(mainParentNode.Element.Id))
                {
                    mainParentNode.ChildNodes.Add(new TreeNode<LocationModel>(location));
                    locations.Remove(location);
                }
                else
                {
                    foreach (var child in mainParentNode.ChildNodes)
                    BuildTree(child, locations);
                }
            }
        }

        private TreeNode<LocationModel> FindMainTreeRootNode(List<LocationModel> locations)
        {
            TreeNode<LocationModel> mainParentNode = null;
            foreach (var location in locations.Reverse<LocationModel>())
            {
                if (location.ParentLocation == 0)
                {
                    mainParentNode = new TreeNode<LocationModel>(location);
                    locations.Remove(location);
                }
            }
            return mainParentNode;
        }
    }
}
