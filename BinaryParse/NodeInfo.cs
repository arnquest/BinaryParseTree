using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryParse
{
    public class NodeInfo
    {
        public BNode Node;
        public string Text;
        public int StartPos;
        public int Size { get { return Text.Length; } }
        public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
        public NodeInfo Parent, Left, Right;
    }
}
