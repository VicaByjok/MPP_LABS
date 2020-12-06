using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AssemblyInformation
{
    public class Node 
    {
        public string inf { private set; get; }
        public Node parent { private set; get; }
        public List<Node> children = new List<Node>();

        public Node(string inf, Node parent)
        {
            this.inf = inf;
            this.parent = parent;
        }
    }
}
