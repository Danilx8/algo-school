using AlgorithmsDataStructures2;

namespace Graph_dfs_test
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
            //CollectionAssert.AreEqual(new Vertex<int>[] { new Vertex<int>(1), new Vertex<int>(2), new Vertex<int>(3), new Vertex<int>(4) }, graph.DepthFirstSearch(1, 4));
            CollectionAssert.AreEqual(Array.Empty<Vertex<int>>(), graph.DepthFirstSearch(0, 4));
            graph.DepthFirstSearch(1, 4);
        }
    }
}