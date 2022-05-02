/*Node is part of a tree, it can be sub node or main/root node*/
namespace BinaryParse
{
    public class Node
    {
        public int item;
        public string value;
        public Node right;
        public Node left;
        public bool current;
        public bool hasChildtoLeft;
        public bool hasChildtoRight;

        public Node(int item, string value)
        {
            this.item = item;
            this.value = value;
            this.current = false;
            this.hasChildtoLeft = false;
            this.hasChildtoRight = false;
        }
    }
}
