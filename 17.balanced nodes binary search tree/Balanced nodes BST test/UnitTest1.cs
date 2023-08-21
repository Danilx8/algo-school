using AlgorithmsDataStructures2;

namespace Balanced_nodes_BST_test
{
    [TestClass]
    public class UnitTest1
    {
        BalancedBST oddLengthTree = new BalancedBST();
        BalancedBST evenLengthTree = new BalancedBST();

        [TestInitialize]
        public void TestInitialize()
        {
            oddLengthTree.GenerateTree(new int[] { 90, 25, 40, 200, 100, 50, 10 });
            evenLengthTree.GenerateTree(new int[] { 90, 25, 40, 200, 50, 100, 10, 250 });
        }

        [TestMethod]
        public void CheckLevels()
        {
            Assert.AreEqual(0, oddLengthTree.Root.Level);

            BSTNode node = oddLengthTree.Root;

            int currentLevel = 1;
            while (node.LeftChild != null)
            {
                node = node.LeftChild;
                Assert.AreEqual(currentLevel, node.Level);
                ++currentLevel;
            }

            node = oddLengthTree.Root;
            currentLevel = 1;
            while (node.RightChild != null)
            {
                node = node.RightChild;
                Assert.AreEqual(currentLevel, node.Level);
                ++currentLevel;
            }

            Assert.AreEqual(0, evenLengthTree.Root.Level);

            node = evenLengthTree.Root;

            currentLevel = 1;
            while (node.LeftChild != null)
            {
                node = node.LeftChild;
                Assert.AreEqual(currentLevel, node.Level);
                ++currentLevel;
            }

            node = evenLengthTree.Root;
            currentLevel = 1;
            while (node.RightChild != null)
            {
                node = node.RightChild;
                Assert.AreEqual(currentLevel, node.Level);
                ++currentLevel;
            }
        }

        [TestMethod]
        public void CheckNodesOrder()
        {
            BSTNode node = oddLengthTree.Root;
            Assert.IsTrue(IsInOrder(node));

            node = evenLengthTree.Root;
            Assert.IsTrue(IsInOrder(node));
        }

        private bool IsInOrder(BSTNode node)
        {
            if (node == null || node.RightChild == null && node.LeftChild == null)
            {
                return true;
            }

            if (node.LeftChild != null && node.LeftChild.NodeKey >= node.NodeKey)
            {
                return false;
            }

            if (node.RightChild != null && node.RightChild.NodeKey < node.NodeKey)
            {
                return false;
            }

            return IsInOrder(node.LeftChild) && IsInOrder(node.RightChild);
        }

        [TestMethod]
        public void CheckTreeBalance()
        {
            BSTNode node = oddLengthTree.Root;
            Assert.IsTrue(OddCheckEveryNode(node, oddLengthTree.IsBalanced(node)));

            node = evenLengthTree.Root;
            Assert.IsTrue(EvenCheckEveryNode(node, evenLengthTree.IsBalanced(node)));
        }

        private bool OddCheckEveryNode(BSTNode node, bool isBalanced)
        {
            if (!isBalanced)
            {
                return false;
            }

            if (node.LeftChild != null)
            {
                OddCheckEveryNode(node.LeftChild, oddLengthTree.IsBalanced(node.LeftChild));
            }

            if (node.RightChild != null)
            {
                OddCheckEveryNode(node.RightChild, oddLengthTree.IsBalanced(node.RightChild));
            }

            return true;
        }

        private bool EvenCheckEveryNode(BSTNode node, bool isBalanced)
        {
            if (!isBalanced)
            {
                return false;
            }

            if (node.LeftChild != null)
            {
                EvenCheckEveryNode(node.LeftChild, evenLengthTree.IsBalanced(node.LeftChild));
            }

            if (node.RightChild != null)
            {
                EvenCheckEveryNode(node.RightChild, evenLengthTree.IsBalanced(node.RightChild));
            }

            return true;
        }

        [TestMethod]
        public void CheckUnbalancedTree()
        {
            BSTNode node = new BSTNode(0, null);
            BSTNode root = node;
            for (int i = 1; i < 10; ++i)
            {
                BSTNode newNode = new BSTNode(i, node);
                node.RightChild = newNode;
                node = newNode;
            }

            Assert.IsFalse(oddLengthTree.IsBalanced(root));
        }
    }
}