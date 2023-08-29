using AlgorithmsDataStructures2;

namespace Graph_bfs_test
{
    [TestClass]
    public class UnitTest1
    {
        SimpleGraph<int> graph;

        [TestInitialize]
        public void TestMethod1()
        {
            graph = new(5);
            for (int i = 0; i < 5; ++i)
            {
                graph.AddVertex(i);
            }

            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(3, 4);
        }

        [TestMethod]
        public void CheckPath()
        {
            //CollectionAssert.AreEqual(new Vertex<int>[] { new Vertex<int>(2), new Vertex<int>(3), new Vertex<int>(4) }, graph.BreadthFirstSearch(2, 4));
            CollectionAssert.AreEqual(Array.Empty<Vertex<int>>(), graph.BreadthFirstSearch(0, 4));
            graph.BreadthFirstSearch(1, 4);
        }
    }
}