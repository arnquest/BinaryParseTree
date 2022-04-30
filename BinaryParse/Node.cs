using System;
using System.Collections.Generic;
using System.Text;
/*Node is part of a tree, it can be sub node or main/root node*/
namespace BinaryParse
{
    public class Node
    {
        public int item;
        public string value;
        public Node right;
        public Node left;

        public Node(int item, string value)
        {
            this.item = item;
            this.value = value;
        }
    }
}
