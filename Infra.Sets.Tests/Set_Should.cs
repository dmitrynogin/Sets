using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Infra.Sets.Universe;

namespace Infra.Sets.Tests
{
    [TestClass]
    public class Set_Should
    {
        Set<int> Negative => Set<int>(i => i < 0);
        Set<int> Positive => Set<int>(i => i > 0);

        [TestMethod]
        public void Be_Truthy()
        {
            Assert.IsTrue(Positive[1]);
        }

        [TestMethod]
        public void Be_Falsy()
        {
            Assert.IsFalse(Positive[-1]);
        }
        
        [TestMethod]
        public void Be_Invertable()
        {
            Assert.IsTrue((!Positive)[-1]);
        }

        [TestMethod]
        public void Be_Subject_Of_Union()
        {
            var s = Positive | Negative;
            Assert.IsTrue(s[-1]);
            Assert.IsFalse(s[0]);
            Assert.IsTrue(s[1]);
        }

        [TestMethod]
        public void Be_Subject_Of_Intersection()
        {
            var s = !Positive & !Negative;
            Assert.IsFalse(s[-1]);
            Assert.IsTrue(s[0]);
            Assert.IsFalse(s[1]);
        }

        [TestMethod]
        public void Have_Iterable_Intersection()
        {
            foreach (var i in Positive[1])
                return;

            Assert.Fail("Set intersection is not enumerable.");
        }

        [TestMethod]
        public void Have_None_Iterable_Intersection()
        {
            foreach (var i in Positive[-1])
                Assert.Fail("Set intersection should not be enumerable.");
        }

        [TestMethod]
        public void Test_Enumeration()
        {
            var values = new[] { -2, -1, 0, 1, 2 };
            foreach (var i in (!Positive & !Negative)[values])
                if (i == 0)
                    return;
                else
                    Assert.Fail("Set intersection can not be properly enumerated.");

            Assert.Fail("Set intersection can not be properly enumerated.");
        }

        [TestMethod]
        public void Be_Extractable()
        {
            var values = new[] { -2, -1, 0, 1, 2 };
            foreach (var i in (!Negative - Positive)[values])
                if (i == 0)
                    return;
                else
                    Assert.Fail("Set intersection can not be properly enumerated.");

            Assert.Fail("Set intersection can not be properly enumerated.");
        }
    }
}
