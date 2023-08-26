using AlgorithmsDataStructures2;
using System.Globalization;

namespace Even_forest_test
{
    [TestClass]
    public class UnitTest1
    {
        SimpleTree<int> emptyTree;
        SimpleTree<int> exampleTree;
        SimpleTree<int> broadTree;
        SimpleTree<int> deepTree;
        SimpleTree<int> smallTree;

        [TestInitialize]
        public void TestInitialize()
        {
            emptyTree = new SimpleTree<int>(null);

            SimpleTreeNode<int> exampleRoot = new SimpleTreeNode<int>(1, null);
            exampleTree = new SimpleTree<int>(exampleRoot);
            SimpleTreeNode<int> exampleTwoChild = new SimpleTreeNode<int>(2, exampleRoot);
            SimpleTreeNode<int> exampleThreeChild = new SimpleTreeNode<int>(3, exampleRoot);
            SimpleTreeNode<int> exampleSixChild = new SimpleTreeNode<int>(6, exampleRoot);
            exampleTree.AddChild(exampleRoot, exampleTwoChild);
            exampleTree.AddChild(exampleRoot, exampleThreeChild);
            exampleTree.AddChild(exampleRoot, exampleSixChild);
            SimpleTreeNode<int> exampleFiveChild = new SimpleTreeNode<int>(5, exampleTwoChild);
            SimpleTreeNode<int> exampleSevenChild = new SimpleTreeNode<int>(7, exampleTwoChild);
            exampleTree.AddChild(exampleTwoChild, exampleFiveChild);
            exampleTree.AddChild(exampleTwoChild, exampleSevenChild);
            SimpleTreeNode<int> exampleFourChild = new SimpleTreeNode<int>(4, exampleThreeChild);
            exampleTree.AddChild(exampleThreeChild, exampleFourChild);
            SimpleTreeNode<int> exampleEightChild = new SimpleTreeNode<int>(8, exampleSixChild);
            exampleTree.AddChild(exampleSixChild, exampleEightChild);
            SimpleTreeNode<int> exampleNineChild = new SimpleTreeNode<int>(9, exampleEightChild);
            SimpleTreeNode<int> exampleTenChild = new SimpleTreeNode<int>(10, exampleEightChild);
            exampleTree.AddChild(exampleEightChild, exampleNineChild);
            exampleTree.AddChild(exampleEightChild, exampleTenChild);

            SimpleTreeNode<int> broadRoot = new SimpleTreeNode<int>(1, null);
            broadTree = new SimpleTree<int>(broadRoot);
            for (int i = 2; i < 10; ++i)
            {
                SimpleTreeNode<int> newNode = new SimpleTreeNode<int>(i, broadRoot);
                broadTree.AddChild(broadRoot, newNode);
                for (int j = i + 10; j < 100; j += 10)
                {
                    SimpleTreeNode<int> grandChild = new SimpleTreeNode<int>(j, newNode);
                    broadTree.AddChild(newNode, grandChild);
                }
            }
            SimpleTreeNode<int> descendant = new SimpleTreeNode<int>(1_000_000, broadRoot);
            broadTree.AddChild(broadRoot, descendant);

            SimpleTreeNode<int> deepParent = new SimpleTreeNode<int>(1, null);
            deepTree = new SimpleTree<int>(deepParent);
            for (int i = 2; i < 11; ++i)
            {
                SimpleTreeNode<int> newNode = new SimpleTreeNode<int>(i, deepParent);
                deepTree.AddChild(deepParent, newNode);
                deepParent = newNode;
            }

            SimpleTreeNode<int> smallRoot = new SimpleTreeNode<int>(1, null);
            SimpleTreeNode<int> smallChild = new SimpleTreeNode<int>(2, smallRoot);
            smallTree = new SimpleTree<int>(smallRoot);
            smallTree.AddChild(smallChild, smallRoot);
        }

        [TestMethod]
        public void CheckMethod()
        {
            CollectionAssert.AreEqual(Array.Empty<int>(), emptyTree.EvenTrees());

            CollectionAssert.AreEqual(new int[] { 1, 3, 1, 6 }, exampleTree.EvenTrees());

            CollectionAssert.AreEqual(new int[] { 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 9 }, broadTree.EvenTrees());

            CollectionAssert.AreEqual(new int[] { 2, 3, 4, 5, 6, 7, 8, 9 }, deepTree.EvenTrees());

            CollectionAssert.AreEqual(Array.Empty<int>(), smallTree.EvenTrees());
        }
    }
}