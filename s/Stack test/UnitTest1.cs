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
            foreach (int value in ints)
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
            foreach (int value in ints)
            {
                stack.Push(value);
            }
            Array.Reverse(ints);
            CollectionAssert.AreEqual(ints, new int[] { stack.Pop(), stack.Pop(), stack.Pop() });
        }

        [TestMethod]
        [DataRow("()")]
        [DataRow("(())")]
        [DataRow("(()())")]
        [DataRow("(()((())()))")]
        [DataRow("(()()(()))")]
        public void BracketsTestTrue(string brackets)
        {
            Assert.IsTrue(Stack<char>.BalancedBrackets(brackets));
        }

        [TestMethod]
        [DataRow("())(")]
        [DataRow("))((")]
        [DataRow("((())")]
        public void BracketsTestFalse(string brackets)
        {
            Assert.IsFalse(Stack<char>.BalancedBrackets(brackets));
        }
    }

    [TestClass]
    public class VersatileTests
    {
        VersatileQueue emptyQueue;
        VersatileQueue exclusiveQueue;
        VersatileQueue inclusiveQueue;

        const int LENGTH = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyQueue = new VersatileQueue();
            exclusiveQueue = new VersatileQueue();
            inclusiveQueue = new VersatileQueue();

            for (int i = 0; i < LENGTH; ++i)
            {
                exclusiveQueue.Push(i);
            }

            inclusiveQueue.Push(10);
            inclusiveQueue.Push("10");
            inclusiveQueue.Push(10.0);
            inclusiveQueue.Push(new int[] { 1, 2, 3, 4, 5 });
        }

        [TestMethod]
        public void EmptySize()
        {
            Assert.AreEqual(0, emptyQueue.Size());
        }

        [TestMethod]
        public void ExclusivePop()
        {
            for (int i = 0; i < LENGTH; ++i)
            {
                Assert.AreEqual(i, exclusiveQueue.Pop());
            }
            Assert.AreEqual(0, exclusiveQueue.Size());
        }

        [TestMethod]
        public void InclusivePop()
        {
            Assert.AreEqual(10, inclusiveQueue.Pop());
            Assert.AreEqual("10", inclusiveQueue.Pop());
            Assert.AreEqual(10.0, inclusiveQueue.Pop());
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5 }, (int[])inclusiveQueue.Pop());
        }
    }

    [TestClass]
    public class CalculationsTest
    {
        [TestMethod]
        [DataRow("1 2 + 3 * =")]
        public void CalculationsMethodTest(string expression)
        {
            Assert.AreEqual(9, Stack<char>.StacksCalculations(expression));
        }
    }
}