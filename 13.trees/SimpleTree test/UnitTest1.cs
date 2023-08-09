using AlgorithmsDataStructures2;

namespace SimpleTree_test
{
    [TestClass]
    public class UnitTest1
    {
        SimpleTree<int> tree;
        SimpleTreeNode<int> parentNode;

        [TestInitialize]
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

            CollectionAssert.AreEqual(new SimpleTreeNode<int>[] { childNode }, tree.FindNodesByValue(childNode.NodeValue));
        }

        [TestMethod]
        public void RemoveNode()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            tree.DeleteNode(childNode);

            List<SimpleTreeNode<int>> emptyList = new List<SimpleTreeNode<int>>();
            CollectionAssert.AreEqual(emptyList, tree.FindNodesByValue(childNode.NodeValue));
            CollectionAssert.AreEqual(emptyList, tree.FindNodesByValue(grandChildNode.NodeValue));
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

            CollectionAssert.AreEqual(new SimpleTreeNode<int>[] { values[0] }, tree.FindNodesByValue(childNode.NodeValue));
            CollectionAssert.AreEqual(new SimpleTreeNode<int>[] { values[1] }, tree.FindNodesByValue(grandChildNode.NodeValue));
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

        [TestMethod]
        public void CountNodes()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            Assert.AreEqual(3, tree.Count());
        }

        [TestMethod]
        public void CountLeaves()
        {
            SimpleTreeNode<int> childNode = new SimpleTreeNode<int>(1, parentNode);
            SimpleTreeNode<int> grandChildNode = new SimpleTreeNode<int>(2, parentNode);

            tree.AddChild(tree.Root, childNode);
            tree.AddChild(childNode, grandChildNode);

            Assert.AreEqual(1, tree.LeafCount());
        }

        [TestMethod]
        public void EmptyTree()
        {
            SimpleTreeNode<object> node = null; 
            SimpleTree<object> emptyTree = new SimpleTree<object>(node);

            List<SimpleTreeNode<int>> emptyList = new List<SimpleTreeNode<int>>();
            CollectionAssert.AreEqual(new List<SimpleTreeNode<object>> { null }, emptyTree.GetAllNodes());
            CollectionAssert.AreEqual(emptyList, emptyTree.FindNodesByValue(0));
        }

        [TestMethod]
        public void AddToEmpty()
        {
            SimpleTree<int> emptyTree = new SimpleTree<int>(null);
            SimpleTreeNode<int> node = new SimpleTreeNode<int>(12, null);
            emptyTree.AddChild(null, node);
            CollectionAssert.AreEqual(new List<SimpleTreeNode<int>> { node }, emptyTree.GetAllNodes());
        }
    }
}