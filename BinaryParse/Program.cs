using System;
using System.Collections.Generic;
using AngouriMath;
using AngouriMath.Extensions;
using System.Text.RegularExpressions;
using System.Linq;

namespace BinaryParse
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Please enter your expression: ");

            //char[] expression = Console.ReadLine().ToString().ToCharArray();
            //string[] expr = Regex.Split(Console.ReadLine().ToString().Trim(), string.Empty);
            string input = "(2/(4-5))";
            string[] expr = Regex.Split(input, string.Empty);
            // Entity expr = Console.ReadLine().Trim().ToString();
            //Console.WriteLine(expr.EvalNumerical());


            Tree btr = new Tree();
            btr.ConstructTree(expr);


            /*btr.Add(2, "(");
            btr.Add(1, "3");
            btr.Add(3, "*");
            btr.Add(6, "+");
            btr.Add(4, ")");
            btr.Add(7, ")");*/
            btr.Print();            
        }
        
    }
}
