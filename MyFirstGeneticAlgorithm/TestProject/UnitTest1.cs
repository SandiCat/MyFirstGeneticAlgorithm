using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string decoded = "22+-72";

            Type needed = GetType(decoded[0]) == Type.Number ? Type.Operator : Type.Number;
            for (int i = 1; i < decoded.Length; i++)
            {
                if (GetType(decoded[i]) != needed)
                {
                    decoded.Remove(i);
                    i--;
                }

                needed = GetType(decoded[i]) == Type.Number ? Type.Operator : Type.Number;
            }

            Assert.AreEqual(decoded, "2+7");
        }

        enum Type
        {
            Operator,
            Number
        }
        static Type GetType(char character)
        {
            List<char> operators = new List<char> { '+', '-', '*', '/' };

            return operators.Contains(character) ? Type.Operator : Type.Number;
        }
    }
}
