﻿using AngouriMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngouriMath.Core.Numerix;

namespace UnitTests.Algebra
{
    [TestClass]
    public class IntegrationTest
    {
        static readonly Entity.Variable x = nameof(x);
        [TestMethod]
        public void Test1()
        {
            var expr = x;
            Assert.IsTrue(Entity.Number.Abs(expr.DefiniteIntegral(x, 0, 1).Real - 1.0/2) < 0.1);
        }
        [TestMethod]
        public void Test2()
        {
            var expr = MathS.Sin(x);
            Assert.AreEqual(0, expr.DefiniteIntegral(x, -1, 1));
        }
        [TestMethod]
        public void Test3()
        {
            var expr = MathS.Sin(x);
            Assert.IsTrue(expr.DefiniteIntegral(x, 0, 3).Real > 1.5);
        }
    }
}
