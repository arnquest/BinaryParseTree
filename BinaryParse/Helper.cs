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
                ")$", RegexOptions.IgnoreCase);

        public static bool IsNumeric(string value)
        {
            return _isNumericRegex.IsMatch(value);
        }
        public static int RegexIndexOf(this string str, string pattern)
        {
            var m = Regex.Match(str, pattern);
            return m.Success ? m.Index : -1;
        }
    }
}
