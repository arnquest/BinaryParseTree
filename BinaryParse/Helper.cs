using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BinaryParse
{
    public static class Helper
    {
        static readonly Regex _isNumericRegex =
        new Regex("^(" +
                /*Hex*/ @"0x[0-9a-f]+" + "|" +
                /*Bin*/ @"0b[01]+" + "|" +
                /*Oct*/ @"0[0-7]*" + "|" +
                /*Dec*/ @"((?!0)|[-+]|(?=0+\.))(\d*\.)?\d+(e\d+)?" +
                ")$");
        public static bool IsNumeric(string value)
        {
            return _isNumericRegex.IsMatch(value);
        }
        public static List<string> ExpressionParser(string input)
        {
            // remove empty space
            input = input.Replace(" ", "");

            if (input == null) return null;

            List<string> result = new List<string>();
            foreach (var match in Regex.Matches(input, @"([*+/\-)(])|([0-9.]+|.)"))
            {
                result.Add(match.ToString());
            }
            return result;
        }
    }
}
