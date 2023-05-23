namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        LinkedList2 emptyList;
        LinkedList2 singleElementList;
        LinkedList2 identicalElementsList;
        LinkedList2 variousElementsList;
        const int LENGTH = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyList               = new LinkedList2();
            singleElementList       = new LinkedList2();
            identicalElementsList   = new LinkedList2();
            variousElementsList     = new LinkedList2();

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
            singleElementList.Remove(1);
            singleElementList.InsertAfter(afterNode, node);
            Assert.IsNotNull(singleElementList.Find(2));
        }

        [TestMethod]
        public void Insert()
        {

        }
    }
}