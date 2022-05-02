using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryParse
{
    // Import the extension method namespace.
    using CustomExteions;
    using System.Reflection;

    /* 
    + 1. If the current token is a '(', add a new node as the left child of the current node, and descend to the left child.
    + 2. If the current token is in the list ['+','-','/','*'], set the root value of the current node to the operator represented by the current token.Add a new node as the right child of the current node and descend to the right child.
    + 3. If the current token is a number, set the root value of the current node to the number and return to the parent.
    + 4. If the current token is a ')', go to the parent of the current node.
    */
    public class TreeParser
    {
        List<string> OPERATORS = new List<string>
        { "+", "-" , "*", "/"};

        List<string> OPERANDS = new List<string>
        { "(", ")"};


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
        public TreeParser()
        {
            _root = null;
            _count = 0;
        }
        public bool ConstructTree(string expr)
        {
            // if the expression is empty, we can't proceed
            if (string.IsNullOrEmpty(expr)) { return false; }

            if (ExpEvaluator.HasTwoParts(expr))
            {
                Expression exp = new Expression();
                exp.Parent = ExpEvaluator.GetParent(expr);
                exp.ChildLeft = ExpEvaluator.GetLeftChild(expr);
                exp.ChildRight = ExpEvaluator.GetRightChild(expr);

                if (_root == null)
                {
                    _root = new Node(0, exp.Parent);
                    _root.current = true;
                    _count++;
                    AddActiveNodeLeft(_root, 0, "", true);
                    Add_Right_Node(_root, 0, "", false);
                }
                for (int i = 0; i < exp.ChildLeft.Count; i++)
                {
                    if (leftParen.Equals(exp.ChildLeft[i]))
                    {
                        AddActiveNodeLeft(_root, i+1, "", true);
                    }
                    else if (Helper.IsNumeric(exp.ChildLeft[i]))
                    {
                        UpdateActiveNodeValue(_root, i+1, exp.ChildLeft[i]);
                        MoveUpParent(_root);
                    }
                    else if (OPERATORS.Contains(exp.ChildLeft[i]))
                    {
                        UpdateActiveNodeValue(_root, i+1, exp.ChildLeft[i]);
                        AddActiveNodeRight(_root, i+1, "", true);
                    }
                }
            }
            return true;
        }
        public bool MoveUpParent(Node node, bool isLeft = true)
        {
            if (node == null) { return false; }
            // no more node to move up
            if (node.current) { return true; }

            if (isLeft)
            {
                if (node.hasChildtoLeft)
                {
                    if (node.left.current)
                    {
                        node.left.current = false;
                        //move the parent up
                        node.current = true;
                        return true;
                    }
                    else if (node.hasChildtoRight && node.item > 0)
                    {
                        if (node.right.current)
                        {
                            node.right.current = false;
                            //move the parent up
                            node.current = true;
                            return true;
                        }
                        else if (node.right.hasChildtoLeft)
                        {
                            return MoveUpParent(node.right, isLeft);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (node.left.hasChildtoLeft)
                    {
                        return MoveUpParent(node.left, isLeft);
                    }
                    else
                    { 
                        return false;
                    }
                }
                else if (node.right.hasChildtoLeft)
                { 
                    return MoveUpParent(node.right, isLeft);
                }
                else
                {
                    return false;
                }
            }
            else 
            { 
                return false; 
            }
        }
        public bool UpdateActiveNodeValue(Node node, int item, string value, bool isLeft = true)
        {
            if (node == null)
                return false;

            if (node.current) { node.value = value; return true; }
            else if (node.hasChildtoLeft) { if (node.left.current) { node.left.value = value; return true; } }

            if (node.hasChildtoRight)
            {
                if (node.right.current)
                {
                    node.left.value = value;
                    return true;
                }
                else
                {
                    if (node.right.hasChildtoRight)
                    {
                        return UpdateActiveNodeValue(node.right, item, value, isLeft);
                    }
                    else
                    {
                        return UpdateActiveNodeValue(node.left, item, value, isLeft);
                    }
                }
            }

            if (item > 0)
            {
                if (node.hasChildtoRight)
                {
                    if (node.right.hasChildtoLeft) { if (node.right.left.current) { node.left.left.value = value; return true; } }
                    else if (node.right.hasChildtoRight) { if (node.right.right.current) { node.right.right.value = value; } }
                    else if (node.right.right.hasChildtoLeft) { if (node.right.right.left.current) { node.right.right.left.value = value; return true; } }
                    else if (node.right.right.hasChildtoRight) { if (node.right.right.right.current) { node.right.right.right.value = value; return true; } }
                    else { return UpdateActiveNodeValue(node.right, item, value, isLeft); }
                }
                else { return UpdateActiveNodeValue(node.left, item, value, isLeft); }
            }
            return false;
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

        public bool AddActiveNodeRight(Node node, int item, string value, bool status, bool isLeft = true)
        {
            if (node == null)
                return false;

            if (node.current) { return UpdateNodeAndAddRight(node, item, value, status, isLeft); }
            else if (node.hasChildtoLeft) { if (node.left.current) { return UpdateNodeAndAddRight(node.left, item, value, status, isLeft); } }

            if (node.hasChildtoRight)
            {
                if (node.right.current)
                {
                    return UpdateNodeAndAddRight(node.right, item, value, status, isLeft);
                }
                else
                {
                    if (node.right.hasChildtoRight)
                    {
                        return AddActiveNodeRight(node.right, item, value, status, isLeft);
                    }
                    else
                    {
                        return AddActiveNodeRight(node.left, item, value, status, isLeft);
                    }
                }
            }

            if (item > 0)
            {
                if (node.hasChildtoRight)
                {
                    if (node.right.hasChildtoLeft) { if (node.right.left.current) { return UpdateNodeAndAddRight(node.right.left, item, value, status, isLeft); } }
                    else if (node.right.hasChildtoRight) { if (node.right.right.current) { return UpdateNodeAndAddRight(node.right.right, item, value, status, isLeft); } }
                    else if (node.right.right.hasChildtoLeft) { if (node.right.right.left.current) { return UpdateNodeAndAddRight(node.right.right.left, item, value, status, isLeft); } }
                    else if (node.right.right.hasChildtoRight) { if (node.right.right.right.current) { return UpdateNodeAndAddRight(node.right.right.right, item, value, status, isLeft); } }
                    else { return AddActiveNodeLeft(node.right, item, value, status, isLeft); }
                }
                else { return AddActiveNodeLeft(node.left, item, value, status, isLeft); }
            }
            return false;
        }
        public bool UpdateNodeAndAddLeft(Node node, int item, string value, bool status, bool isLeft = true)
        {
            node.current = !status;
            return Add_Left_Node(node, item, value, status);
        }
        public bool UpdateNodeAndAddRight(Node node, int item, string value, bool status, bool isLeft = true)
        {
            node.current = !status;
            return Add_Right_Node(node, item, value, status);
        }
        public bool AddActiveNodeLeft(Node node, int item, string value, bool status, bool isLeft = true)
        {
            if (node == null)
                return false;

            if (node.current) { return UpdateNodeAndAddLeft(node, item, value, status, isLeft); }
            else if (node.hasChildtoLeft) { if (node.left.current) { return UpdateNodeAndAddLeft(node.left, item, value, status, isLeft); } }

            if (node.hasChildtoRight)
            {
                if (node.right.current)
                {
                    return UpdateNodeAndAddLeft(node.right, item, value, status, isLeft);
                }
                else
                {
                    if (node.right.hasChildtoRight)
                    {
                        return AddActiveNodeLeft(node.right, item, value, status, isLeft);
                    }
                    else
                    {
                        return AddActiveNodeLeft(node.left, item, value, status, isLeft);
                    }
                }
            }

            if (item > 0)
            {
                if (node.hasChildtoRight)
                {
                    if (node.right.hasChildtoLeft) { if (node.right.left.current) { return UpdateNodeAndAddLeft(node.right.left, item, value, status, isLeft); } }
                    else if (node.right.hasChildtoRight) { if (node.right.right.current) { return UpdateNodeAndAddLeft(node.right.right, item, value, status, isLeft); } }
                    else if (node.right.right.hasChildtoLeft) { if (node.right.right.left.current) { return UpdateNodeAndAddLeft(node.right.right.left, item, value, status, isLeft); } }
                    else if (node.right.right.hasChildtoRight) { if (node.right.right.right.current) { return UpdateNodeAndAddLeft(node.right.right.right, item, value, status, isLeft); } }
                    else { return AddActiveNodeLeft(node.right, item, value, status, isLeft); }
                }
                else { return AddActiveNodeLeft(node.left, item, value, status, isLeft); }
            }
            return false;
        }
        public bool Add_Left_Node(Node node, int item, string value, bool status)
        {
            if (node.left == null)
            {
                node.left = new Node(item, value);
                node.hasChildtoLeft = true;
                _count++;
                node.left.current = status;
                return true;
            }
            else
            {
                node.left.current = !status;
                return Add_Left_Node(node.left, item, value, status);
            }
        }
        public bool Add_Right_Node(Node node, int item, string value, bool status)
        {
            if (node.right == null)
            {
                node.right = new Node(item, value);
                node.hasChildtoRight = true;
                _count++;
                node.right.current = status;
                return true;
            }
            else
            {
                node.right.current = !status;
                return Add_Right_Node(node.right, item, value, status);
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
