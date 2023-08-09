using AlgorithmsDataStructures2;

namespace SimpleTree_test
{
    [TestClass]
    public class UnitTest1
    {
        SimpleTree<int> tree;
        SimpleTreeNode<int> parentNode;

        [ClassInitialize]
        public void CreateTree()
        {
            parentNode = new SimpleTreeNode<int>(0, null);
            tree = new SimpleTree<int>(parentNode);
        }

        [TestMethod]
        public void AddNewNode()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            tree.AddChild(tree.Root, childNode);

            Assert.AreEqual(new SimpleTreeNode<int>[] { childNode }, tree.FindNodesByValue(childNode.NodeValue));
        }

        [TestMethod]
        public void RemoveNode()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            tree.DeleteNode(childNode);
            Assert.IsNull(tree.FindNodesByValue(childNode.NodeValue));
            Assert.IsNull(tree.FindNodesByValue(grandChildNode.NodeValue));
        }

        [TestMethod]
        public void FindNodesByValue()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            List<SimpleTreeNode<int>> values = new()
            {
                childNode,
                grandChildNode
            };

            Assert.AreEqual(values[0], tree.FindNodesByValue(childNode.NodeValue));
            Assert.AreEqual(values[1], tree.FindNodesByValue(grandChildNode.NodeValue));
        }

        [TestMethod]
        public void MoveNode()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            tree.MoveNode(grandChildNode, tree.Root);
            Assert.IsTrue(tree.Root.Children.Contains(grandChildNode));
            Assert.IsFalse(tree.FindNodesByValue(childNode.NodeValue).Contains(grandChildNode));
        }

        public void CountNodes()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            Assert.AreEqual(3, tree.Count());
        }

        public void CountLeaves()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            Assert.AreEqual(2, tree.LeafCount());
        }
    }
}