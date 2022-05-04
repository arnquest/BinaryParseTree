using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BinaryParse
{
    public class ExpEvaluator
    {
        static readonly Regex _hasTwoParts =
            new Regex("\\)\\*\\(|\\)\\+\\(|\\)-\\(|\\)/\\(",
             RegexOptions.IgnoreCase);


        static List<string> OPERATORS = new List<string>
        { "+", "-" , "*", "/"};

        static string leftParen = "(";
        static string rightParen = ")";

        /*

        */
        /// <summary>
        /// Purpose: Find the value of math expression 
        /// Step 1: Create an operand stack.
        /// Step 2: If the character is an operand (0-9), push it to the operand stack.
        /// Step 3: If the character is an operator, pop two operands from the stack, operate and push the result back to the stack.
        /// Step 4: After the entire expression has been traversed, pop the final result from the stack.
        /// </summary>
        /// <param name="expr"></param>
        /// <returns>result for the given expression</returns>
        public static int Evaluate(List<string> expr)
        {
            try {
                Stack<int> operands = new Stack<int>(); // Operands stack (0-9)
                Stack<string> operations = new Stack<string>();  //Operator stack 

                foreach (string e in expr)
                {
                    if (Helper.IsNumeric(e))
                    {
                        operands.Push(Int32.Parse(e));
                    }
                    else if (e.Equals(leftParen))
                    {
                        operations.Push(e);
                    }
                    //Closed parenthesis, evaluate the entire parenthesis
                    else if (e.Equals(rightParen))
                    {
                        while (operations.Peek() != leftParen)
                        {
                            int output = performOperation(operands, operations);
                            operands.Push(output);   //push result back to stack
                        }
                        operations.Pop();
                    }
                    // current character is operator
                    else if (OPERATORS.Contains(e))
                    {
                        while (operations.Count != 0 && precedence(e) <= precedence(operations.Peek()))
                        {
                            int output = performOperation(operands, operations);
                            operands.Push(output);   //push result back to stack
                        }
                        operations.Push(e);   //push the current operator to stack
                    }
                }
                while (operations.Count != 0)
                {
                    int output = performOperation(operands, operations);
                    operands.Push(output);   //push final result back to stack
                }
                return operands.Pop();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return 0;
            }
 
        }
        public static int performOperation(Stack<int> operands, Stack<string> operations)
        {
            int a = operands.Pop();
            int b = operands.Pop();
            string operation = operations.Pop();
            switch (operation)
            {
                case "+":
                    return a + b;
                case "-":
                    return b - a;
                case "*":
                    return a * b;
                case "/":
                    if (a == 0)
                    {
                        Console.Out.WriteLine("Cannot divide by zero");
                        return 0;
                    }
                    return b / a;
            }
            return 0;
        }

        static int precedence(string s)
        {
            switch (s)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
            }
            return -1;
        }

        public static List<string> ExpressionParser(string input)
        {
            try
            {
                if (input == null) return null;

                // remove empty space
                input = input.Replace(" ", "");

                bool brackets = ContainsBrackets(input);
                if (brackets == false)
                {
                    input = "(" + input + ")";
                }

                List<string> result = new List<string>();

                foreach (var match in Regex.Matches(input, @"([*+/\-)(])|([0-9.]+|.)"))
                {
                    result.Add(match.ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public static bool ContainsBrackets(string expr)
        {
            if (expr.StartsWith(leftParen) && expr.EndsWith(rightParen))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool HasTwoParts(string expr)
        {
            return _hasTwoParts.IsMatch(expr);
        }
        public static string GetParent(string expr)
        {
            int splitIndex = Helper.RegexIndexOf(expr, _hasTwoParts.ToString());
            return expr.Substring(splitIndex + 1, 1);
        }
        public static List<string> GetLeftChild(string expr)
        {
            int splitIndex = Helper.RegexIndexOf(expr, _hasTwoParts.ToString());
            return ExpressionParser(expr.Substring(0, splitIndex + 1));
        }
        public static List<string> GetRightChild(string expr)
        {
            int splitIndex = Helper.RegexIndexOf(expr, _hasTwoParts.ToString());
            return ExpressionParser(expr.Substring(splitIndex + 2, expr.Length - splitIndex - 2));
        }
    }
}
