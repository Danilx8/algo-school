using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AlgorithmsDataStructures2
{
    public class Vertex<T>
    {
        public bool Hit;
        public T Value;
        public Vertex(T val)
        {
            Value = val;
            Hit = false;
        }
    }

    public class SimpleGraph<T>
    {
        public Vertex<T>[] vertex;
        public int[,] m_adjacency;
        public int max_vertex;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int[size, size];
            vertex = new Vertex<T>[size];
        }

        public void AddVertex(T value)
        {
            // ваш код добавления новой вершины 
            // с значением value 
            // в свободную позицию массива vertex

            for (int i = 0; i < vertex.Length; ++i)
            {
                if (vertex[i] == null)
                {
                    vertex[i] = new Vertex<T>(value);
                    break;
                }
            }
        }

        // здесь и далее, параметры v -- индекс вершины
        // в списке  vertex
        public void RemoveVertex(int v)
        {
            vertex[v] = default;

            for (int i = 0; i < max_vertex; ++i)
            {
                if (m_adjacency[v, i] == 1)
                {
                    m_adjacency[v, i] = m_adjacency[i, v] = 0;
                }
            }
        }

        public bool IsEdge(int v1, int v2)
        {
            // true если есть ребро между вершинами v1 и v2
            return m_adjacency[v1, v2] != 0;
        }

        public void AddEdge(int v1, int v2)
        {
            // добавление ребра между вершинами v1 и v2
            m_adjacency[v1, v2] = 1;
            m_adjacency[v2, v1] = 1;
        }

        public void RemoveEdge(int v1, int v2)
        {
            // удаление ребра между вершинами v1 и v2
            m_adjacency[v1, v2] = 0;
            m_adjacency[v2, v1] = 0;
        }

        public List<Vertex<T>> DepthFirstSearch(int VFrom, int VTo)
        {
            // Узлы задаются позициями в списке vertex.
            // Возвращается список узлов -- путь из VFrom в VTo.
            // Список пустой, если пути нету.

            foreach (var vertex in vertex)
            {
                vertex.Hit = false;
            }

            return InDepthSearch(VFrom, VTo, new Stack<Vertex<T>>()).Reverse().ToList();
        }

        private Stack<Vertex<T>> InDepthSearch(int currentIndex, int goal, Stack<Vertex<T>> memoization)
        {
            memoization.Push(vertex[currentIndex]);
            vertex[currentIndex].Hit = true;

            if (m_adjacency[currentIndex, goal] == 1)
            {
                memoization.Push(vertex[goal]);
                return memoization;
            }

            for (int i = 0; i < max_vertex; ++i)
            {
                if (m_adjacency[currentIndex, i] == 1 && !vertex[i].Hit)
                {
                    InDepthSearch(i, goal, memoization);
                }

                if (memoization.Peek() == vertex[goal])
                {
                    return memoization;
                }
            }

            memoization.Pop();
            return memoization;
        }

        public List<Vertex<T>> BreadthFirstSearch(int VFrom, int VTo)
        {
            // узлы задаются позициями в списке vertex.
            // возвращает список узлов -- путь из VFrom в VTo
            // или пустой список, если пути нету

            foreach (var vertex in vertex)
            {
                vertex.Hit = false;
            }

            Queue<int[]> nonVisited = new Queue<int[]>();
            nonVisited.Enqueue(new int[] { VFrom });
            vertex[VFrom].Hit = true;
            while (nonVisited.Count != 0)
            {
                int[] current = nonVisited.Dequeue();

                if (current[current.Length - 1] == VTo)
                {
                    List<Vertex<T>> result = new List<Vertex<T>>();
                    foreach (int index in current)
                    {
                        result.Add(vertex[index]);
                    }

                    return result;
                }

                for (int i = 0; i < max_vertex; ++i)
                {
                    if (m_adjacency[current[current.Length - 1], i] == 1 && !vertex[i].Hit)
                    {
                        nonVisited.Enqueue(current.Append(i).ToArray());
                        vertex[i].Hit = true;
                    }
                }
            }

            return new List<Vertex<T>>();
        }

        public List<Vertex<T>> WeakVertices()
        {
            // возвращает список узлов вне треугольников
            return null;
        }
    }
}