using AlgorithmsDataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ordered_list_test
{
    [TestClass]
    public class UnitTest1
    {
        public OrderedList<int> emptyAscList;
        public OrderedList<int> emptyDescList;
        public OrderedList<int> hugeAscList;
        public OrderedList<int> hugeDescList;
        public OrderedList<int> singleList;

        [TestInitialize]
        public void Initialize() 
        {
            emptyAscList = new OrderedList<int>(true);
            emptyDescList = new OrderedList<int>(false);
            hugeAscList = new OrderedList<int>(true);
            hugeDescList = new OrderedList<int>(false);
            singleList = new OrderedList<int>(true);

            singleList.Add(1);

            for (int i = 0; i < 10; ++i)
            {
                hugeAscList.Add(i);
                hugeDescList.Add(i);
            }
        }

        [TestMethod]
        public void EmptyAscAdd()
        {
            emptyAscList.Add(1);
            emptyAscList.Add(10);
            
            List<Node<int>> nodeList = emptyAscList.GetAll();
            List<int> numbersList = new List<int>();

            foreach (Node<int> node in nodeList)
            {
                numbersList.Add(node.value);
            }

            CollectionAssert.AreEqual(new int[] { 1, 10 }, numbersList);
        }

        [TestMethod]
        public void AscInputInbetween()
        {
            hugeAscList.Add(7);

            List<Node<int>> nodeList = hugeAscList.GetAll();
            List<int> numbersList = new List<int>();

            foreach (Node<int> node in nodeList)
            {
                numbersList.Add(node.value);
            }

            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 7, 8, 9 }, numbersList);
        }

        [TestMethod]
        public void DescInputInbetween()
        {
            hugeDescList.Add(8);

            List<Node<int>> nodeList = hugeDescList.GetAll();
            List<int> numbersList = new List<int>();

            foreach (Node<int> node in nodeList)
            {
                numbersList.Add(node.value);
            }

            CollectionAssert.AreEqual(new int[] { 9, 8, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, numbersList);
        }

        [TestMethod]
        public void AscEndDelete()
        {
            hugeAscList.Delete(hugeAscList.tail.value);
            Assert.AreEqual(hugeAscList.tail.value, 8);
        }

        [TestMethod]
        public void AsBeginDelete()
        {
            hugeAscList.Delete(0);
            Assert.AreEqual(hugeAscList.head.value, 1);
        }

        [TestMethod]
        public void DescEndDelete()
        {
            hugeDescList.Delete(0);
            Assert.AreEqual(hugeDescList.tail.value, 1);
        }

        [TestMethod]
        public void DescBegDelete()
        {
            hugeDescList.Delete(9);
            Assert.AreEqual(hugeDescList.head.value, 8);
        }

        [TestMethod]
        public void DeleteInbetween()
        {
            hugeAscList.Delete(8);
            Assert.AreEqual(7, hugeAscList.tail.prev.value);
        }

        [TestMethod]
        public void DeleteSingle()
        {
            singleList.Delete(1);
            Assert.AreEqual(0, singleList.Count());
        }

        [TestMethod]
        public void DeleteEmpty()
        {
            emptyAscList.Delete(0);
            Assert.AreEqual(0, emptyAscList.Count());
        }
    }
}