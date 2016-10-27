using System;
using System.Collections.Generic;

namespace CAlConverter
{

    internal class StringTree
    {
        public List<StringNode> Nodes { get; set; }
        public StringNode BaseNode { get { return baseNode; } set { setBaseNode(value); } }
        private StringNode baseNode;

        public StringTree()
        {
            Nodes = new List<StringNode>();
        }

        internal void Add(StringNode stringNode)
        {
            throw new NotImplementedException();
        }

        internal void Add(int parent, StringNode stringNode)
        {
            Nodes.Add(stringNode);
            Nodes.Find(n => n.OrderNumber == parent)?.Add(stringNode);
        }

        private void setBaseNode(StringNode node)
        {
            node.OrderNumber = 0;
            Nodes.Add(node);
            baseNode = node;
        }
    }
}