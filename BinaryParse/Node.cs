using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Node is part of a tree, it can be sub node or main/root node*/
namespace BinaryParse
{
    public class Node
    {
        public int item { get; set; }
        public string value { get; set; }
        public Node right { get; set; }
        public Node left { get; set; }
        public Node parentNode { get; set; }
        public Node currentNode { get; set; }


        public Node(int item, string value)
        {
            this.item = item;
            this.value = value;
        }
        //public override string ToString()
        //{
        //    return GetType().GetProperties()
        //        .Select(info => (info.Name, Value: info.GetValue(this, null) ?? "(null)"))
        //        .Aggregate(
        //            new StringBuilder(),
        //            (sb, pair) => sb.AppendLine($"{pair.Name}: {pair.Value}"),
        //            sb => sb.ToString());
        //}
    }
}
