using BinaryParse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BinaryParseTest
{
    [TestClass]
    public class BinaryParseTest
    {
        [TestMethod]
        public void TestExpressionEvaluator()
        {
            List<string> expr = ExpEvaluator.ExpressionParser("(7+3) * (5-2)");
            int expected = 30;
            int result = ExpEvaluator.Evaluate(expr);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TextExpressionParser()
        {
            List<string> lstExpected = new List<string>
            { "(", "15", "*", "(", "1", "+", "1", ")", ")", "+", "(", "2", "-", "1", ")"};

            List<string> lstOutput = ExpEvaluator.ExpressionParser("(15*(1+1))+(2-1)");
            for (int i = 0; i < lstExpected.Count; i++)
            {
                Assert.AreEqual(lstExpected[i], lstOutput[i]);
            }
        }
    }
}
