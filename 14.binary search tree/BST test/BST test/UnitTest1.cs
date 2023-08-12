using AlgorithmsDataStructures2;

namespace BST_test
{
    [TestClass]
    public class UnitTest1
    {
        BST<int> tree;
        BST<int> emptyTree;
        BSTNode<int> root;

        [TestInitialize]
        public void TestInitialize()
        {
            root = new BSTNode<int>(0, 0, null);
            tree = new BST<int>(root);
            emptyTree = new BST<int>(null);
        }

        [TestMethod]
        public void AddLeft()
        {
            Assert.IsFalse(tree.FindNodeByKey(-1).NodeHasKey);
            tree.AddKeyValue(-1, 1);
            BSTFind<int> firstSupposedResult = tree.FindNodeByKey(-1);
            Assert.IsTrue(firstSupposedResult.NodeHasKey);

            Assert.IsFalse(emptyTree.FindNodeByKey(0).NodeHasKey);
            emptyTree.AddKeyValue(0, 0);
            BSTFind<int> secondSupposedResult = emptyTree.FindNodeByKey(0);
            Assert.IsTrue(secondSupposedResult.NodeHasKey);
        }

        [TestMethod]
        public void AddRight()
        {
            tree.AddKeyValue(1, 1);
            BSTFind<int> firstSupposedResult = tree.FindNodeByKey(1);
            Assert.IsTrue(firstSupposedResult.NodeHasKey);
            tree.DeleteNodeByKey(10);
        }

        [TestMethod]
        public void AddExistingKey()
        {
            Assert.IsFalse(tree.AddKeyValue(0, 0));
        }

        [TestMethod]
        public void RootMaxSearch()
        {
            for (int i = 0; i < 10; ++i)
            {
                tree.AddKeyValue(i, i);
                Assert.AreEqual(i, tree.FinMinMax(root, true).NodeKey);
            }
            tree.DeleteNodeByKey(9);
        }

        [TestMethod]
        public void RootMinSearch()
        {
            for (int i = 1; i < 11; ++i)
            {
                tree.AddKeyValue(i, i);
                Assert.AreEqual(0, tree.FinMinMax(root, false).NodeKey);
            }
            tree.DeleteNodeByKey(0);
        }

        [TestMethod]
        public void NodeMaxSearch()
        {
            tree.AddKeyValue(12, 12);
            for (int i = 0; i < 10; ++i)
            {
                tree.AddKeyValue(i, i);  
            }
            BSTNode<int> subNode = tree.FindNodeByKey(9).Node;
            Assert.AreEqual(9, tree.FinMinMax(subNode, true).NodeKey);
            tree.DeleteNodeByKey(9);
        }

        [TestMethod]
        public void NodeMinSearch()
        {
            tree.AddKeyValue(12, 12);
            for (int i = 0; i < 10; ++i)
            {
                tree.AddKeyValue(i, i);
            }
            BSTNode<int> subNode = tree.FindNodeByKey(2).Node;
            Assert.AreEqual(2, tree.FinMinMax(subNode, false).NodeKey);
            tree.DeleteNodeByKey(2);
        }

        [TestMethod]
        public void NodeRemoval()
        {
            tree.AddKeyValue(10, 10);
            tree.AddKeyValue(9, 9);
            tree.AddKeyValue(8, 8);
            tree.AddKeyValue(12, 12);
            tree.AddKeyValue(15, 15);
            tree.AddKeyValue(11, 11);
            tree.AddKeyValue(13, 13);

            Assert.AreEqual(8, tree.Count());
            Assert.IsTrue(tree.FindNodeByKey(10).NodeHasKey);
            tree.DeleteNodeByKey(10);
            Assert.AreEqual(7, tree.Count());
            Assert.AreEqual(11, tree.FindNodeByKey(0).Node.RightChild.NodeKey);
        }

        [TestMethod]
        public void Counter()
        {
            Assert.AreEqual(0, emptyTree.Count());
            Assert.AreEqual(1, tree.Count());

            tree.AddKeyValue(1, 1);
            tree.AddKeyValue(-1, -1);
            tree.AddKeyValue(15, 15);
            Assert.AreEqual(4, tree.Count());
        }
    }
}