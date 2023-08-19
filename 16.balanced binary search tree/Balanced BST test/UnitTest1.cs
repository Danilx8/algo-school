using AlgorithmsDataStructures2;

namespace Balanced_BST_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OddLengthTest()
        {
            int[] input = { 90, 25, 40, 200, 100, 50, 10 };

            int[] expectedOutput = { 50, 25, 100, 10, 40, 90, 200 };

            CollectionAssert.AreEqual(expectedOutput, BalancedBST.GenerateBBSTArray(input));
        }

        [TestMethod]
        public void EvenLengthTest()
        {
            int[] input = { 90, 25, 40, 200, 50, 100, 10, 250};

            int[] expectedOutput = new int[15];

            expectedOutput[0] = 50;
            expectedOutput[1] = 25;
            expectedOutput[2] = 100;
            expectedOutput[3] = 10;
            expectedOutput[4] = 40;
            expectedOutput[5] = 90;
            expectedOutput[6] = 250;
            expectedOutput[13] = 200;

            CollectionAssert.AreEqual(expectedOutput, BalancedBST.GenerateBBSTArray(input));
        }
    }
}