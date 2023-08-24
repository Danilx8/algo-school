using AlgorithmsDataStructures2;

namespace Heap_test
{
    [TestClass]
    public class UnitTest1
    {
        AlgorithmsDataStructures2.Heap emptyHeap = new();
        AlgorithmsDataStructures2.Heap bigHeap = new();
        int[] bigHeapArray;

        [TestInitialize]
        public void TestMethod1()
        {
            bigHeap.MakeHeap(new int[] { 28, 9, 132, 5, 32, 8, 6 }, 2);
            bigHeapArray = new int[] { 132, 32, 28, 5, 9, 8, 6 };
        }

        [TestMethod] 
        public void TestGetHeap()
        {
            CollectionAssert.AreEqual(bigHeap.HeapArray, bigHeapArray);
        }

        [TestMethod]
        public void GetMax()
        {
            Assert.AreEqual(bigHeapArray[0], bigHeap.GetMax());
            Assert.AreEqual(bigHeapArray[1], bigHeap.HeapArray[0]);
        }

        [TestMethod]
        public void AddBigValue()
        {
            emptyHeap.MakeHeap(new int[] { 3, 5, 4 }, 2);
            emptyHeap.Add(100);
            Assert.AreEqual(100, emptyHeap.GetMax());
        }

        [TestMethod]
        public void AddSmallValue()
        {
            emptyHeap.MakeHeap(new int[] { 3, 5, 4 }, 2);
            emptyHeap.Add(1);
            Assert.AreEqual(1, emptyHeap.HeapArray[3]);
        }
    }
}