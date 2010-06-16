using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Readify.Puzzles.Triangle.Test
{
    [TestClass]
    public class TestReadifyPuzzlesTriangle
    {
        #region Error Handling
        [TestMethod]
        public void TestErrorValidationZero()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(1, 1, 0), TriangleType.Error);
        }

        [TestMethod]
        public void TestErrorValidationNegative()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(1, 1, -1), TriangleType.Error);
        }
        #endregion

        #region Test Equlateral Triangle
        [TestMethod]
        public void TestEquilateralAllOnes()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(1, 1, 1), TriangleType.Equilateral);
        }

        [TestMethod]
        public void TestEquilateralAllThrees()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(3, 3, 3), TriangleType.Equilateral);
        }
        #endregion

        #region Test Isosceles Triangle
        [TestMethod]
        public void TestIsosceles1()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(1, 1, 3), TriangleType.Isossceles);
        }

        [TestMethod]
        public void TestIsosceles2()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(1, 3, 1), TriangleType.Isossceles);
        }
        
        [TestMethod]
        public void TestIsosceles3()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(3, 1, 1), TriangleType.Isossceles);
        }
        #endregion

        #region Test Scalene Triangle
        [TestMethod]
        public void TestScalene1()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(1, 2, 3), TriangleType.Scalene);
        }
        [TestMethod]
        public void TestScalene2()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(1, 3, 2), TriangleType.Scalene);
        }
        [TestMethod]
        public void TestScalene3()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(2, 3, 1), TriangleType.Scalene);
        }

        [TestMethod]
        public void TestScalene4()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(2, 1, 3), TriangleType.Scalene);
        }
        [TestMethod]
        public void TestScalene5()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(3, 1, 2), TriangleType.Scalene);
        }
        [TestMethod]
        public void TestScalene6()
        {
            Triangles tri = new Triangles();
            Assert.AreEqual(tri.GetTriangleType(3, 2, 1), TriangleType.Scalene);
        }
        #endregion
    }
}
