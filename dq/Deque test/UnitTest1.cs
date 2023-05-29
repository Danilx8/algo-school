namespace AlgorithmsDataStructures
{
    [TestClass]
    public class DequeTest
    {
        Deque<int> emptyDeque;
        Deque<int> singleDeque;
        Deque<int> bigDeque;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyDeque = new Deque<int>();
            singleDeque = new Deque<int>();
            bigDeque = new Deque<int>();

            singleDeque.AddFront(1);

            for (int i = 0; i < 10; ++i)
            {
                if (i % 2 == 0) bigDeque.AddFront(i);
                else bigDeque.AddTail(i);
            }
        }

        [TestMethod]
        public void EmptyDequeSize()
        {
            Assert.AreEqual(0, emptyDeque.Size());
        }

        [TestMethod]
        public void SingleAddDifference()
        {
            CollectionAssert.AreEqual(new int[] { 1, 1 }, 
                new int[] { singleDeque.Size(), singleDeque.RemoveTail() });
        }

        [TestMethod]
        public void DifferentAdditions()
        {
            emptyDeque.AddFront(12);
            Deque<int> newEmptyDeque = new Deque<int>();

            newEmptyDeque.AddTail(12);
            Assert.AreEqual(emptyDeque.RemoveTail(), newEmptyDeque.RemoveFront());
        }
    }
}