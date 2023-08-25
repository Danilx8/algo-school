using AlgorithmsDataStructures2;
using System.Reflection.Metadata.Ecma335;

namespace Graph_test
{
    [TestClass]
    public class UnitTest1
    {
        SimpleGraph graph;
        Vertex[] verteces;

        [TestInitialize]
        public void TestMethod1()
        {
            graph = new(5);
            verteces = new Vertex[5];
            for (int i = 0; i < 5; ++i)
            {
                graph.AddVertex(i);
                verteces[i] = new Vertex(i);
            }
        }

        [TestMethod]
        public void MakeEdges()
        {
            for (int i = 0; i < verteces.Length; ++i)
            {
                for (int j = 0; j < verteces.Length; ++j)
                {
                    Assert.IsFalse(graph.IsEdge(i, j));
                }
            }

            graph.AddEdge(1, 2);
            graph.AddEdge(3, 4);

            Assert.IsTrue(graph.IsEdge(1, 2));
            Assert.IsTrue(graph.IsEdge(3, 4));
        }

        [TestMethod]
        public void RemoveEdges()
        {
            graph.AddEdge(1, 2);
            graph.AddEdge(3, 4);

            graph.RemoveEdge(1, 2);
            graph.RemoveEdge(3, 4);

            Assert.IsFalse(graph.IsEdge(2, 1));
            Assert.IsFalse(graph.IsEdge(4, 3));
        }

        [TestMethod]
        public void RemoveVerteces()
        {
            graph.RemoveVertex(4);

            Assert.IsNull(graph.vertex[4]);
            for (int i = 0; i < 4; ++i)
            {
                Assert.IsNotNull(graph.vertex[i]);
            }

            graph.AddEdge(2, 3);
            graph.AddEdge(3, 1);
            graph.RemoveVertex(3);

            Assert.IsFalse(graph.IsEdge(3, 2));
            Assert.IsFalse(graph.IsEdge(1, 3));
        }
    }
}