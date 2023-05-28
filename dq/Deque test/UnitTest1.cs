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
                bigDeque.AddFront(i);
            }
        }

        [TestMethod]
        public void emptyDequeSize()
        {
            Assert.AreEqual(0, emptyDeque.Size());
        }

        [TestMethod]
        public void singleAddDifference()
        {
            CollectionAssert.AreEqual(new int[] { 1, 1 }, 
                new int[] { singleDeque.Size(), singleDeque.RemoveTail() });
        }
    }
}