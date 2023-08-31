using AlgorithmsDataStructures2;

namespace Weak_vertices_test
{
    [TestClass]
    public class UnitTest1
    {
        SimpleGraph<int> firstGraph;
        SimpleGraph<int> secondGraph;
        SimpleGraph<int> thirdGraph;

        [TestInitialize]
        public void TestInitialize()
        {
            firstGraph = new SimpleGraph<int>(6);
            for (int i = 0; i < 6; ++i)
            {
                firstGraph.AddVertex(i);
            }
            firstGraph.AddEdge(0, 1);
            firstGraph.AddEdge(1, 2);
            firstGraph.AddEdge(2, 3);
            firstGraph.AddEdge(2, 4);
            firstGraph.AddEdge(3, 4);
            firstGraph.AddEdge(4, 5);

            secondGraph = new SimpleGraph<int>(3);
            for (int i = 0; i < 3; ++i)
            {
                secondGraph.AddVertex(i);
            }
            secondGraph.AddEdge(0, 1);
            secondGraph.AddEdge(1, 2);

            thirdGraph = new SimpleGraph<int>(3);
            for (int i = 0; i < 3; ++i)
            {
                thirdGraph.AddVertex(i);
            }
            thirdGraph.AddEdge(0, 1);
            thirdGraph.AddEdge(1, 2);
            thirdGraph.AddEdge(2, 0);
        }

        [TestMethod]
        public void CheckWeakNodes()
        {
            //CollectionAssert.AreEqual(new Vertex<int>[] { new Vertex<int>(0), new Vertex<int>(1), new Vertex<int>(5) }, firstGraph.WeakVertices());
            //CollectionAssert.AreEquivalent(new Vertex<int>[] { new Vertex<int>(0), new Vertex<int>(1), new Vertex<int>(2) }, secondGraph.WeakVertices());
            CollectionAssert.AreEquivalent(new Vertex<int>[] { }, thirdGraph.WeakVertices());
        }
    }
}