using AlgorithmsDataStructures;

namespace Queue_test
{
    [TestClass]
    public class UnitTest1
    {
        AlgorithmsDataStructures.Queue<int> emptyQueue;
        AlgorithmsDataStructures.Queue<int> bigQueue;
        const int LENGTH = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyQueue = new AlgorithmsDataStructures.Queue<int>();
            bigQueue = new AlgorithmsDataStructures.Queue<int>();

            for (int i = 0; i < LENGTH; ++i)
            {
                bigQueue.Enqueue(i);
            }
        }

        [TestMethod]
        public void EmptyEnqueue()
        {
            emptyQueue.Enqueue(10);
            Assert.AreEqual(1, emptyQueue.Size());
        }

        [TestMethod]
        public void ClearQueue()
        {
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, LENGTH - 3 }, 
                new int[] { bigQueue.Dequeue(), bigQueue.Dequeue(), bigQueue.Dequeue(), bigQueue.Size() });
        }
    }
}