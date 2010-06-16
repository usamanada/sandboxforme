using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Readify.Puzzles.ReverseWord.Test
{
    [TestClass]
    public class TestReadifyPuzzlesReverseStringHelper
    {
        [TestMethod]
        public void TestNull()
        {
            Assert.AreEqual(StringHelper.ReverseWords(null), "");
        }

        [TestMethod]
        public void TestEmptyString()
        {
            Assert.AreEqual(StringHelper.ReverseWords(""), "");
        }

        [TestMethod]
        public void TestCat()
        {
            Assert.AreEqual(StringHelper.ReverseWords("Cat"), "taC");
        }

        [TestMethod]
        public void TestComplexString()
        {
            Assert.AreEqual(StringHelper.ReverseWords("cat and dog"), "tac dna god");
        }
    }
}
