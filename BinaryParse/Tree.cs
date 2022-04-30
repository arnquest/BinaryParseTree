using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryParse
{
    // Import the extension method namespace.
    using CustomExteions;

    /* 
    + 1. If the current token is a '(', add a new node as the left child of the current node, and descend to the left child.
    + 2. If the current token is in the list ['+','-','/','*'], set the root value of the current node to the operator represented by the current token.Add a new node as the right child of the current node and descend to the right child.
    + 3. If the current token is a number, set the root value of the current node to the number and return to the parent.
    + 4. If the current token is a ')', go to the parent of the current node.
    */
    public class Tree
    {
        List<string> OPERATORS = new List<string>
        { "+", "-" , "*", "/"};

        string operatorAdd = "+";
        string operatorSub = "-";
        string operatorMul = "*";
        string operatorDiv = "/";
        string leftParen = "(";
        string rightParen = ")";

        private Node _root;
        public Node Root {
            get { return _root; } 
        }
        private int _count;

        private IComparer<int> _comparer = Comparer<int>.Default;

        public void Print()
        {
            // call the extension method
            Root.Print();
        }
        public Tree()
        {
            _root = null;
            _count = 0;
        }

        public bool ConstructTree(string[] expr) 
        {
            // if the expression is empty, we can't construct a tree
            if (expr == null) { return false;}

            if (_root == null)
            {
                createRoot("");
                string stage = "LeftNode";
                int numOutput;

                for (int i = 1; i < expr.Length - 2; i++)
                {
                    switch (stage)
                    {
                        case "Root":
                            if (OPERATORS.Contains(expr[i]))
                            {
                                _root.value = expr[i];
                                _root.right = new Node(0, "");
                                stage = "RightNode";
                            }
                            break;

                        case "LeftNode":
                            if (leftParen.Equals(expr[i]))
                            {
                                if (_root.left == null) { _root.left = new Node(0, ""); }
                            }
                            else if (Int32.TryParse(expr[i], out numOutput))
                            {
                                _root.left.value = expr[i];
                                stage = "Root";
                            }

                            break;

                        case "RightNode":
                            if (leftParen.Equals(expr[i]))
                            {
                                if (_root.right.left == null) { _root.right.left = new Node(0, ""); stage = "ChildLeft"; }
                            }
                            break;

                        case "ChildLeft":
                            if (Int32.TryParse(expr[i], out numOutput)) 
                            { 
                                _root.right.left.value = expr[i]; 
                            }
                            else if (OPERATORS.Contains(expr[i])) 
                            { 
                                _root.right.value = expr[i];
                                if (_root.right.right == null) { _root.right.right = new Node(0, ""); }
                                stage = "ChildRight";
                            }
                            break;
                        case "ChildRight":
                            if (Int32.TryParse(expr[i], out numOutput))
                            {
                                _root.right.right.value = expr[i];
                            }
                            break;
                    }
                }
                
                
            }
            return true;
        }

        public bool createRoot(string value)
        {
            _root = new Node(0, value);
            _count++;
            return true;
        }
        public bool Add(int Item, string value)
        {
            if (_root == null)
            {
                _root = new Node(Item, value);
                _count++;
                return true;
            }
            else
            {
                return Add_Sub(_root, Item, value);
            }
        }
        private bool Add_Left_Node(Node node, string value)
        {
            if (node.left == null)
            {
                node.left = new Node(0, value);
                _count++;
                return true;
            }
            else
            {
                return Add_Left_Node(node.right, value);
            }
        }
        private bool Add_Right_Node(Node node, string value)
        {
            if (node.right == null)
            {
                node.right = new Node(0, value);
                _count++;
                return true;
            }
            else
            {
                return Add_Right_Node(node.right, value);
            }
        }
        private bool Add_Sub(Node Node, int Item, string value)
        {
            if (_comparer.Compare(Node.item, Item) < 0)
            {
                if (Node.right == null)
                {
                    Node.right = new Node(Item, value);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(Node.right, Item, value);
                }
            }
            else if (_comparer.Compare(Node.item, Item) > 0)
            {
                if (Node.left == null)
                {
                    Node.left = new Node(Item, value);
                    _count++;
                    return true;
                }
                else
                {
                    return Add_Sub(Node.left, Item, value);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
