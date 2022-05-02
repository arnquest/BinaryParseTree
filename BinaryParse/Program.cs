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
            Console.WriteLine("Please enter your expression: ");

            string input = Console.ReadLine();

            List<string> expr = ExpEvaluator.ExpressionParser(input);
            int ans = ExpEvaluator.Evaluate(expr);

            Console.WriteLine("Answer for the expression is: " + ans.ToString());

            TreeParser tree = new TreeParser();
            tree.ConstructTree(input);
            
            tree.Print();            
        }
        
    }
}
