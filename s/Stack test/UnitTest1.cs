namespace AlgorithmsDataStructures
{
    [TestClass]
    public class UnitTest1
    {
        Stack<int> stack;

        [TestInitialize]
        public void InitializeStack()
        {
            stack = new Stack<int>();
        }

        [TestMethod]
        public void EmptySize()
        {
            Assert.AreEqual(0, stack.Size());
        }

        [TestMethod]
        public void PushTest()
        {
            stack.Push(89);
            stack.Push(2);
            stack.Push(1);
            Assert.AreEqual(3, stack.Size());
        }

        [TestMethod]
        [DataRow(new int[] { 89, 2, 1 })]
        public void PeekTest(int[] ints)
        {
            foreach(int value in ints)
            {
                stack.Push(value);
            }
            CollectionAssert.AreEqual(new int[] { 1, 89, 2 }, 
                new int[] { stack.Peek(), stack.head.value, stack.tail.prev.value });
        }

        [TestMethod]
        [DataRow(new int[] { 89, 2, 1 })]
        public void PopTest(int[] ints)
        {
            foreach(int value in ints)
            {
                stack.Push(value);
            }
            Array.Reverse(ints);
            CollectionAssert.AreEqual(ints, new int[] { stack.Pop(), stack.Pop(), stack.Pop()});
        }

        //[TestMethod]
        //[TestMethod]
        //[DataRow(new object[] { 20, "Enter", 3.14 })]
        //public void VariousTypes(object[] objects)
        //{
        //    foreach (object element in objects)
        //    {
        //        stack.Push(element);
        //    }


        //}
    }
}