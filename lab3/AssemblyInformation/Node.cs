using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyInformation
{
    public class Node
    {
        public string value { private set; get; }
        public Node parent { private set; get; }
        internal List<Node> children = new List<Node>();

        internal Node(string value, Node parent)
        {
            this.value = value;
            this.parent = parent;
        }
    }
}
