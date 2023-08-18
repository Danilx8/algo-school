namespace aBST_test
{
    [TestClass]
    public class UnitTest1
    {
        AlgorithmsDataStructures2.aBST tree;
        int TREE_DEPTH = 2;

        [TestInitialize]
        public void TestInitialize()
        {
            tree = new AlgorithmsDataStructures2.aBST(TREE_DEPTH);
            tree.AddKey(50);
            tree.AddKey(100);
            tree.AddKey(90);
            tree.AddKey(200);
            tree.AddKey(25);
            tree.AddKey(10);
            tree.AddKey(40);
        }

        [TestMethod]
        public void ExamineTree()
        {
            Assert.IsNotNull(tree.FindKeyIndex(50));
            Assert.IsNotNull(tree.FindKeyIndex(100));
            Assert.IsNotNull(tree.FindKeyIndex(90));
            Assert.IsNotNull(tree.FindKeyIndex(200));
            Assert.IsNotNull(tree.FindKeyIndex(25));
            Assert.IsNotNull(tree.FindKeyIndex(10));
            Assert.IsNotNull(tree.FindKeyIndex(40));
        }
    }
}