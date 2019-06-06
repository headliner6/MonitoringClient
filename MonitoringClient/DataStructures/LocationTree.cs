using MonitoringClient.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MonitoringClient.DataStructures
{
    public class LocationTree
    {
        public LocationNode RootNode { get; set; }

        public LocationTree()
        {
            RootNode = null;
        }

        public void InsertNode(LocationNode locationNode)
        {
            if (locationNode.Element.ParentLocation.Equals(System.DBNull.Value))
            {
                RootNode = locationNode;
            }
            else if (locationNode.Element.ParentLocation.Equals(RootNode.Element.Id))
            {
                RootNode.ChildeNodes.Add(locationNode);
            }
            else
            {
                InsertNodeIntoParentChildeLocationNode(RootNode.ChildeNodes, locationNode);
            }
        }

        private void InsertNodeIntoParentChildeLocationNode(List<LocationNode> parentChildeNodes, LocationNode locationNode)
        {
            for (int i = 0; i < parentChildeNodes.Count; i++)
            {
                var parentChildeNode = parentChildeNodes[i];
                //if (parentChildeNode == null)
                //{
                //    MessageBox.Show("Parent-Location ist in der Tabelle nicht vorhanden!");
                //}
                if (locationNode.Element.ParentLocation.Equals(parentChildeNode.Element.Id))
                {
                    parentChildeNode.ChildeNodes.Add(locationNode);
                }
                else
                {
                    InsertNodeIntoParentChildeLocationNode(parentChildeNode.ChildeNodes, locationNode);
                }
            }
        }
    }
}
