using System;
using System.Collections.Generic;

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
            if (tree.ConstructTree(input)) 
            {
                tree.Print();
            }

            
        }

    }
}
