using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryParse
{
    // Import the extension method namespace.
    using CustomExteions;
    public class BTree
    {

        private Node _root;
        public Node Root {
            get { return _root; } 
        }
        private int _count;

        private IComparer<int> _comparer = Comparer<int>.Default;

        public void Print()
        {
            Root.Print();
        }
        public BTree()
        {
            _root = null;
            _count = 0;
        }


        public bool Add(int Item)
        {
            if (_root == null)
            {
                _root = new Node(Item);
                _count++;
                return true;
            }
            else
            {
                return Add_Sub(_root, Item);
            }
        }

        private bool Add_Sub(Node Node, int Item)
        {
            if (_comparer.Compare(Node.item, Item) < 0)
            {
                if (Node.right == null)
                {
                    Node.right = new Node(Item);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(Node.right, Item);
                }
            }
            else if (_comparer.Compare(Node.item, Item) > 0)
            {
                if (Node.left == null)
                {
                    Node.left = new Node(Item);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(Node.left, Item);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
