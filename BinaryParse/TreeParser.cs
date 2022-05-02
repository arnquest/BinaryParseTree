using System.Collections.Generic;

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
    public class TreeParser
    {
        List<string> OPERATORS = new List<string>
        { "+", "-" , "*", "/"};

        List<string> OPERANDS = new List<string>
        { "(", ")"};


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
        public bool ConstructTree(string expr)
        {

            return true;
        }
    }
}
