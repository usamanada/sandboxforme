using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Readify.Puzzles.SinglyLinkedList.Test
{
    [TestClass]
    public class TestReadifyPuzzlesSinglyLinedList
    {
        [TestMethod]
        public void ItemsZeroFindTailIndexFive()
        {
            SinglyLinkedList<int> sList = new SinglyLinkedList<int>();
            try
            {
                sList.FindNodeFromtTail(5);
            }
            catch (ArgumentException argEx)
            {
                Assert.AreEqual(argEx.Message, "Index was out of range. Must be non-negative and less than the size of the List.\r\nParameter name: Index\r\nActual value was 5.");
            }
            
        }
        [TestMethod]
        public void ItemsZeroFindTailIndexZero()
        {
            SinglyLinkedList<int> sList = new SinglyLinkedList<int>();
            try
            {
                sList.FindNodeFromtTail(0);
            }
            catch (ArgumentException argEx)
            {
                Assert.AreEqual(argEx.Message, "Index was out of range. Must be non-negative and less than the size of the List.\r\nParameter name: Index\r\nActual value was 0.");
            }
        }
        [TestMethod]
        public void ItemsZeroFindTailIndexNegativeFive()
        {
            SinglyLinkedList<int> sList = new SinglyLinkedList<int>();
            try
            {
                sList.FindNodeFromtTail(-5);
            }
            catch (ArgumentException argEx)
            {
                Assert.AreEqual(argEx.Message, "Index must be greater than or equal to 0\r\nParameter name: Index\r\nActual value was -5.");
            }
        }
        [TestMethod]
        public void ItemsOneFindTailIndexFive()
        {
            SinglyLinkedList<int> sList = new SinglyLinkedList<int>();
            try
            {
                sList.AddNode(0);
                sList.FindNodeFromtTail(5);
            }
            catch (ArgumentException argEx)
            {
                Assert.AreEqual(argEx.Message, "Index was out of range. Must be non-negative and less than the size of the List.\r\nParameter name: Index\r\nActual value was 5.");
            }
        }
        [TestMethod]
        public void ItemsFiveFindTailIndexFive()
        {
            SinglyLinkedList<int> sList = new SinglyLinkedList<int>();
            sList.AddNode(0);
            sList.AddNode(1);
            sList.AddNode(2);
            sList.AddNode(3);
            sList.AddNode(4);
            Node<int> node = sList.FindNodeFromtTail(4);
            Assert.AreEqual(0, node.NodeValue);
        }
        [TestMethod]
        public void ItemsFiveFindTailIndexZero()
        {
            SinglyLinkedList<int> sList = new SinglyLinkedList<int>();
            sList.AddNode(0);
            sList.AddNode(1);
            sList.AddNode(2);
            sList.AddNode(3);
            sList.AddNode(4);
            Node<int> node = sList.FindNodeFromtTail(0);
            Assert.AreEqual(4, node.NodeValue);
        }
        [TestMethod]
        public void ItemsTenFindTailIndexfive()
        {
            SinglyLinkedList<int> sList = new SinglyLinkedList<int>();
            sList.AddNode(0);
            sList.AddNode(1);
            sList.AddNode(2);
            sList.AddNode(3);
            sList.AddNode(4);
            sList.AddNode(5);
            sList.AddNode(6);
            sList.AddNode(7);
            sList.AddNode(8);
            sList.AddNode(9);

            Node<int> node = sList.FindNodeFromtTail(4);
            Assert.AreEqual(5, node.NodeValue);
        }
    }
}
