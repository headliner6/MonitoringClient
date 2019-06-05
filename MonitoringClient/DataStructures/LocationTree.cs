using MonitoringClient.Model;
using System;
using System.Collections.Generic;

namespace MonitoringClient.DataStructures
{
    public class LocationTree
    {
        public Node<LocationsModel> Parent { get; set; }
        private readonly List<Node<LocationsModel>> _childrenNodes;
        public IReadOnlyList<Node<LocationsModel>> ChildrenNodes
        {
            get { return _childrenNodes; }
        }
        public LocationTree()
        {
            Parent = null;
        }

        public void Insert(Node<LocationsModel> location)
        {
            if (Parent.Element.Id == location.Element.Id)
            {
                //Parent.
            }
        }
    }
}
