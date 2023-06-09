using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmsDataStructures
{
    [TestClass]
    public class LinkedListTest
    {
        LinkedList2 emptyList;
        LinkedList2 singleElementList;
        LinkedList2 identicalElementsList;
        LinkedList2 variousElementsList;
        const int LENGTH = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyList             = new LinkedList2();
            singleElementList     = new LinkedList2();
            identicalElementsList = new LinkedList2();
            variousElementsList   = new LinkedList2();

            Node singleNode = new Node(1);
            singleElementList.AddInTail(singleNode);

            for (int i = 0; i < LENGTH; ++i)
            {
                Node node = new Node(1);
                identicalElementsList.AddInTail(node);
            }

            for (int i = 0; i < LENGTH; ++i)
            {
                Node node = new Node(i);
                variousElementsList.AddInTail(node);
            }
        }

        [TestMethod]
        public void EmptyFind()
        {
            Assert.IsNull(emptyList.Find(1));
        }

        [TestMethod]
        public void SingleFindTrue()
        {
            Assert.IsNotNull(singleElementList.Find(1));
        }

        [TestMethod]
        [DataRow(new int[] { 0, 2, 3, 4, 5 })]
        public void SingleFindFalse(int[] numbers)
        {
            bool flag = false;
            for (int i = 0; i < numbers.Length; ++i)
            {
                if (singleElementList.Find(numbers[i]) != null)
                {
                    flag = true;
                    break;
                }
            }
            Assert.AreEqual(flag, false);
        }

        [TestMethod]
        [DataRow(1)]
        public void IdenticalFindTrue(int number)
        {
            identicalElementsList.Clear();
            Assert.IsNull(identicalElementsList.Find(number));
        }

        [TestMethod]
        public void SingleInsert()
        {
            Node afterNode = singleElementList.Find(1);
            Node node = new Node(2);
            singleElementList.InsertAfter(afterNode, node);
            Assert.IsNotNull(singleElementList.Find(2));
        }

        [TestMethod]
        public void SingleRemove()
        {
            singleElementList.RemoveAll(1);
            Assert.IsTrue(singleElementList.Count() == 0);
        }

        [TestMethod]
        public void VariousRemove()
        {
            Assert.IsTrue(variousElementsList.Remove(9));
        }

        [TestMethod]
        public void VariousRemoveAll()
        {
            variousElementsList.RemoveAll(2);
            Assert.IsTrue(variousElementsList.Count() == 9);
        }

        [TestMethod]
        public void VariousInsertInbetween()
        {
            Node node = variousElementsList.Find(2);
            Node afterNode = new Node(2);
            variousElementsList.InsertAfter(node, afterNode);
            for (int j = 0; j < 5; ++j)
            {
                Node newNode = new Node(6);
                variousElementsList.AddInTail(newNode);
            }
            for (int i = 0; i < LENGTH; ++i)
            {
                variousElementsList.RemoveAll(i);
            }

            Node coolNode = new(12);
            variousElementsList.InsertAfter(null, coolNode);
            Assert.IsTrue(variousElementsList.Count() == 1);
        }
    }

    [TestClass]
    public class DummyLinkedListTest
    {
        DummyLinkedList emptyList;
        DummyLinkedList singleElementList;
        DummyLinkedList identicalElementsList;
        DummyLinkedList variousElementsList;

        const int LENGTH = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyList             = new DummyLinkedList();
            singleElementList     = new DummyLinkedList();
            identicalElementsList = new DummyLinkedList();
            variousElementsList   = new DummyLinkedList();

            Node singleNode = new Node(1);
            singleElementList.AddInTail(singleNode);

            for (int i = 0; i < LENGTH; ++i)
            {
                Node node = new Node(1);
                identicalElementsList.AddInTail(node);
            }

            for (int i = 0; i < LENGTH; ++i)
            {
                Node node = new Node(i);
                variousElementsList.AddInTail(node);
            }
        }

        [TestMethod]
        public void AddInTailSingleCheck()
        {
            Assert.AreEqual(1, singleElementList.Count());
        }

        [TestMethod]
        public void AddInTailBigCheck()
        {
            Assert.AreEqual(LENGTH, variousElementsList.Count());
        }

        [TestMethod]
        public void EmptyFind()
        {
            Assert.IsNull(emptyList.Find(0));
        }

        [TestMethod]
        public void BigFind()
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                Assert.IsNotNull(identicalElementsList.Find(1));
                Assert.IsNotNull(variousElementsList.Find(i));
            }
        }

        [TestMethod]
        public void IdenticalFindAll()
        {
            Assert.AreEqual(LENGTH, identicalElementsList.FindAll(1).Count);
        }

        [TestMethod]
        public void EmptyFindAll()
        {
            Assert.AreEqual(0, emptyList.FindAll(0).Count);
        }

        [TestMethod]
        public void VariousFindAll()
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                Assert.AreEqual(1, variousElementsList.FindAll(i).Count);
            }
        }

        [TestMethod]
        public void IdenticalRemove()
        {
            identicalElementsList.Remove(1);
            Assert.AreEqual(LENGTH - 1, identicalElementsList.Count());
        }

        [TestMethod]
        public void IdenticalClear()
        {
            identicalElementsList.Clear();
            Assert.AreEqual(0, identicalElementsList.Count());
        }

        [TestMethod]
        public void IdenticalRemoveAll()
        {
            identicalElementsList.RemoveAll(1);
            Assert.AreEqual(0, identicalElementsList.Count());
        }

        [TestMethod]
        public void VariousRemoveAll()
        {
            for (int i = LENGTH - 1; i >= 0; --i)
            {
                variousElementsList.RemoveAll(i);
                Assert.AreEqual(i, variousElementsList.Count()); 
            }
        }

        [TestMethod]
        public void VariousInsertAfter()
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                Node newNode = new Node(LENGTH - i - 1);
                Node afterNode = variousElementsList.Find(i);
                variousElementsList.InsertAfter(afterNode, newNode);
                Assert.AreEqual(LENGTH + i + 1, variousElementsList.Count());
            }
        }

        [TestMethod]
        public void RemoveIdenticalAfterWrong()
        {
            Node insertNode = new Node(25);
            identicalElementsList.InsertAfter(null, insertNode);
            identicalElementsList.RemoveAll(1);
            Assert.AreEqual(1, identicalElementsList.Count());
        }
    }

    [TestClass]
    public class CircledListTest
    {
        CircledLinkedList emptyList;
        CircledLinkedList singleElementList;
        CircledLinkedList identicalElementsList;
        CircledLinkedList variousElementsList;

        const int LENGTH = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyList             = new CircledLinkedList();
            singleElementList     = new CircledLinkedList();
            identicalElementsList = new CircledLinkedList();
            variousElementsList   = new CircledLinkedList();

            Node singleNode = new Node(1);
            singleElementList.AddInTail(singleNode);

            for (int i = 0; i < LENGTH; ++i)
            {
                Node node = new Node(1);
                identicalElementsList.AddInTail(node);
            }

            for (int i = 0; i < LENGTH; ++i)
            {
                Node node = new Node(i);
                variousElementsList.AddInTail(node);
            }
        }

        [TestMethod]
        public void AddInTailSingleCheck()
        {
            Assert.AreEqual(1, singleElementList.Count());
        }

        [TestMethod]
        public void AddInTailBigCheck()
        {
            Assert.AreEqual(LENGTH, variousElementsList.Count());
        }

        [TestMethod]
        public void EmptyFind()
        {
            Assert.IsNull(emptyList.Find(0));
        }

        [TestMethod]
        public void BigFind()
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                Assert.IsNotNull(identicalElementsList.Find(1));
                Assert.IsNotNull(variousElementsList.Find(i));
            }
        }

        [TestMethod]
        public void IdenticalFindAll()
        {
            Assert.AreEqual(LENGTH, identicalElementsList.FindAll(1).Count);
        }

        [TestMethod]
        public void EmptyFindAll()
        {
            Assert.AreEqual(0, emptyList.FindAll(0).Count);
        }

        [TestMethod]
        public void VariousFindAll()
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                Assert.AreEqual(1, variousElementsList.FindAll(i).Count);
            }
        }

        [TestMethod]
        public void IdenticalRemove()
        {
            identicalElementsList.Remove(1);
            Assert.AreEqual(LENGTH - 1, identicalElementsList.Count());
        }

        [TestMethod]
        public void IdenticalClear()
        {
            identicalElementsList.Clear();
            Assert.AreEqual(0, identicalElementsList.Count());
        }

        [TestMethod]
        public void IdenticalRemoveAll()
        {
            identicalElementsList.RemoveAll(1);
            Assert.AreEqual(0, identicalElementsList.Count());
        }

        [TestMethod]
        public void VariousRemoveAll()
        {
            for (int i = LENGTH - 1; i >= 0; --i)
            {
                variousElementsList.RemoveAll(i);
                Assert.AreEqual(i, variousElementsList.Count());
            }
        }

        [TestMethod]
        public void VariousInsertAfter()
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                Node newNode = new Node(LENGTH - i - 1);
                Node afterNode = variousElementsList.Find(i);
                variousElementsList.InsertAfter(afterNode, newNode);
                Assert.AreEqual(LENGTH + i + 1, variousElementsList.Count());
            }
        }

        [TestMethod]
        public void RemoveIdenticalAfterWrong()
        {
            Node insertNode = new Node(25);
            identicalElementsList.InsertAfter(null, insertNode);
            identicalElementsList.RemoveAll(1);
            Assert.AreEqual(1, identicalElementsList.Count());
        }
    }
}