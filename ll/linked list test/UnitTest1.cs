using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        private LinkedList emptyList;
        private LinkedList oneElementList;
        private LinkedList bigList;
        private LinkedList identicalList;

        private List<Node> onesNodes;
        private List<Node> differentNodes;
        private List<Node> emptyNodes;

        public const int length = 10;

        [TestInitialize]
        public void InitializeLists()
        {
            emptyList       = new LinkedList();
            oneElementList  = new LinkedList();
            bigList         = new LinkedList();
            identicalList   = new LinkedList();
            onesNodes       = new List<Node>();
            differentNodes  = new List<Node>();
            emptyNodes      = new List<Node>();
            Node one        = new Node(1);
            
            oneElementList.AddInTail(one);
            
            for (int i = 0; i < length; ++i)
            {
                Node node = new Node(i);
                bigList.AddInTail(node);
                //differentNodes.Add(node);
            }

            for (int i = 0; i < length; ++i) {
                Node ones = new Node(1);
                identicalList.AddInTail(ones);
                onesNodes.Add(ones);
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow()]
        public void EmptyFind(int _value)
        {
            Assert.IsNull(emptyList.Find(_value));
        }

        [TestMethod]
        [DataRow(1)]
        public void SingleFind(int _value)
        {
            Assert.IsNotNull(oneElementList.Find(_value));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(length - 1)]
        [DataRow(50)]
        public void BigFind(int _value)
        {
            Assert.IsNotNull(bigList.Find(_value));
        }

        [TestMethod]
        [DataRow(1)]
        public void IdenticalFInd(int _value)
        {
            Assert.IsNotNull(identicalList.Find(_value));
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(12)]
        [DataRow()]
        public void EmptyFindAll(int _value)
        {
            bool equality = emptyList.FindAll(_value).SequenceEqual(emptyNodes);
            Assert.IsTrue(equality);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(50)]
        [DataRow(99)]
        public void BigFindAll(int _value)
        {
            differentNodes.Add(bigList.Find(_value));
            bool equality = bigList.FindAll(_value).SequenceEqual(differentNodes);
            Assert.IsTrue(equality);
        }

        [TestMethod]
        [DataRow(1)]
        public void IdenticalFindAll(int _value)
        {
            bool equality = identicalList.FindAll(_value).SequenceEqual(onesNodes);
            Assert.IsTrue(equality);
        }


        [TestMethod]
        public void BigCount()
        {
            int bigLength = bigList.Count();
            Assert.AreEqual(length, bigLength);
        }

        [TestMethod]
        [DataRow(length-1)]
        public void BigRemove(int _value)
        {
            Assert.IsTrue(bigList.Remove(_value));
        }

        [TestMethod]
        [DataRow(1)]
        public void IdenticalRemove(int _value)
        {
            identicalList.RemoveAll(_value);
            Assert.AreEqual(identicalList.Count(), 0);
        }

        [TestMethod]
        public void SingleRemove()
        {
            oneElementList.Remove(1);
            Assert.IsTrue(oneElementList.Count() == 0);
        }

        [TestMethod]
        public void EmptySumm()
        {
            LinkedList list = LinkedList.Summ(emptyList, emptyList);
            Assert.IsTrue(list.head == null && list.tail == null);
        }

        [TestMethod]
        public void bigSumm()
        {
            LinkedList summList = new LinkedList();
            for (int i = 0; i < length; ++i)
            {
                Node summNode = new Node(i + i);
                summList.AddInTail(summNode);
            }

            bool equality = true;
            Node firstNode = summList.head;

            LinkedList funcList = LinkedList.Summ(bigList, bigList);

            Node secondNode = funcList.head;
            while (firstNode != null)
            {
                if (firstNode.value != secondNode.value) 
                { 
                    equality = false; 
                    break; 
                }
                firstNode = firstNode.next;
                secondNode = secondNode.next;
            }
            Assert.IsTrue(equality);
        }

        [TestMethod]
        public void WrongLengthSumm()
        {
            Assert.IsNull(LinkedList.Summ(emptyList, oneElementList));
        }
    }
}