using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.DataStructures
{
    public class TreeNode<T>
    {
        public T Element { get; set; }
        public List<TreeNode<T>> ChildNodes { get; set; }

        public TreeNode(T element)
        {
            this.Element = element;
            this.ChildNodes = new List<TreeNode<T>>();
        }

        public void AddChildNode(T element)
        {
            this.ChildNodes.Add(new TreeNode<T>(element));
        }
    }
}
