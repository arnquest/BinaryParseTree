using System;
using System.Reflection;
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

        public static void GetPropertyValues(Object obj)
        {
            Type t = obj.GetType();
            Console.WriteLine("Type is: {0}", t.Name);
            PropertyInfo[] props = t.GetProperties();
            Console.WriteLine("Properties (N = {0}):",
                              props.Length);
            foreach (var prop in props)
                if (prop.GetIndexParameters().Length == 0)
                    Console.WriteLine("   {0} ({1}): {2}", prop.Name,
                                      prop.PropertyType.Name,
                                      prop.GetValue(obj));
                else
                    Console.WriteLine("   {0} ({1}): <Indexed>", prop.Name,
                                      prop.PropertyType.Name);
        }

        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null) return null;//throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) return null;//throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                //var obj = Activator.CreateInstance(src.GetType());
                return prop != null ? prop.GetValue(src, null) : null;
                
            }
        }
    }
}
