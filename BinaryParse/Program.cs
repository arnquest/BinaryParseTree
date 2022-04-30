using System;
using System.Collections.Generic;
using AngouriMath;
using AngouriMath.Extensions;

namespace BinaryParse
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Please enter your expression: ");

            Entity expr = Console.ReadLine().Trim().ToString();
            Console.WriteLine(expr.EvalNumerical());*/
            BTree btr = new BTree();
            btr.Add(6);
            btr.Add(2);
            btr.Add(7);
            btr.Add(8);
            btr.Add(1);
            btr.Add(9);
            btr.Add(5);
            btr.Print();            
        }
        
    }
}
