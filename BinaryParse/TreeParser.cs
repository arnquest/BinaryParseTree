using System.Collections.Generic;

namespace BinaryParse
{
    // Import the extension method namespace.
    using CustomExteions;
    using System;
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

        private IComparer<int> _comparer = Comparer<int>.Default;

        string leftParen = "(";
        string rightParen = ")";

        private Node _root;
        public Node Root
        {
            get { return _root; }
        }
        private int _count;

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
        /// <summary>
        /// - Construct binary tree uding Node object
        /// - It handles 2 parts expression such as (7+3)*(5-2) or ((15/(7-(1+1)))*3)-(2+(1+1))
        /// - IT can also handle 1 sided expression such as (3+(4*5))
        /// </summary>
        /// <param name="expr"></param>
        /// <returns>true if tree construction is passed otherwise return false if it runs into error</returns>
        public bool ConstructTree(string expr)
        {
            try
            {
                if (string.IsNullOrEmpty(expr)) { return false; }

                if (ExpEvaluator.HasTwoParts(expr))
                {
                    Expression exp = new Expression();
                    exp.Parent = ExpEvaluator.GetParent(expr);
                    exp.ChildLeft = ExpEvaluator.GetLeftChild(expr);
                    exp.ChildRight = ExpEvaluator.GetRightChild(expr);

                    Add_First_Node(0, exp.Parent);
                    Add_Sub_Node(_root.currentNode, 1, "");

                    // construct the left side
                    for (int l = 0; l < exp.ChildLeft.Count; l++)
                    {
                        if (leftParen.Equals(exp.ChildLeft[l]))
                        {
                            Add_Sub_Node(_root.currentNode, l + 2, "");
                        }
                        else if (Helper.IsNumeric(exp.ChildLeft[l]))
                        {
                            _root.currentNode.value = exp.ChildLeft[l];
                        }
                        else if (OPERATORS.Contains(exp.ChildLeft[l]))
                        {
                            _root.currentNode.parentNode.value = exp.ChildLeft[l];
                            Add_Sub_Node(_root.currentNode.parentNode, l + 2, "", false);
                        }
                        else if (rightParen.Equals(exp.ChildLeft[l]))
                        {
                            _root.currentNode = _root.currentNode.parentNode;
                        }
                    }
                    // construct the left side
                    Root.currentNode = _root;
                    Add_Sub_Node(_root.currentNode, 1, "", false);

                    for (int r = 0; r < exp.ChildRight.Count; r++)
                    {
                        if (leftParen.Equals(exp.ChildRight[r]))
                        {
                            Add_Sub_Node(_root.currentNode, r + 2, "");
                        }
                        else if (Helper.IsNumeric(exp.ChildRight[r]))
                        {
                            _root.currentNode.value = exp.ChildRight[r];
                        }
                        else if (OPERATORS.Contains(exp.ChildRight[r]))
                        {
                            _root.currentNode.parentNode.value = exp.ChildRight[r];
                            Add_Sub_Node(_root.currentNode.parentNode, r + 2, "", false);
                        }
                        else if (rightParen.Equals(exp.ChildRight[r]))
                        {
                            _root.currentNode = _root.currentNode.parentNode;
                        }
                    }
                }
                else
                {
                    List<string> simpleExpr = ExpEvaluator.ExpressionParser(expr);
                    Add_First_Node(0, "");
                    for (int i = 0; i < simpleExpr.Count; i++)
                    {
                        if (leftParen.Equals(simpleExpr[i]))
                        {
                            Add_Sub_Node(_root.currentNode, i + 1, "");
                        }
                        else if (Helper.IsNumeric(simpleExpr[i]))
                        {
                            _root.currentNode.value = simpleExpr[i];
                        }
                        else if (OPERATORS.Contains(simpleExpr[i]))
                        {
                            _root.currentNode.parentNode.value = simpleExpr[i];
                            Add_Sub_Node(_root.currentNode.parentNode, i + 1, "", false);
                        }
                        else if (rightParen.Equals(simpleExpr[i]))
                        {
                            _root.currentNode = _root.currentNode.parentNode;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// Adding first node/root node of the tree
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool Add_First_Node(int Item, string value)
        {
            try
            {
                if (_root == null)
                {
                    _root = new Node(Item, value);
                    _root.currentNode = _root;
                    _count++;
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// / Adding sub node to the existing node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="item"></param>
        /// <param name="value"></param>
        /// <param name="isLeft"></param>
        /// <returns></returns>
        private bool Add_Sub_Node(Node node, int item, string value, bool isLeft = true)
        {
            try 
            {
                if (isLeft)
                {
                    if (node.left == null)
                    {
                        node.left = new Node(item, value);
                        _root.currentNode = node.left;
                        node.left.parentNode = node;
                        _count++;
                        return true;
                    }
                    else
                    {
                        return Add_Sub_Node(node.left, item, value, isLeft);
                    }
                }
                else
                {
                    if (node.right == null)
                    {
                        node.right = new Node(item, value);
                        _root.currentNode = node.right;
                        node.right.parentNode = node;
                        _count++;
                        return true;
                    }
                    else
                    {

                        return Add_Sub_Node(node.right, item, value, isLeft);
                    }
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
        
    }
}
