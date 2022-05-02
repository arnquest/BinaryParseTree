using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryParse
{
    public class Expression
    {
        public string Parent;
        public List<string> ChildLeft;
        public List<string> ChildRight;

        public Expression()
        { 
            this.Parent = "";
            this.ChildLeft = new List<string>();
            this.ChildRight = new List<string>();
        }

    }
}
