using System;
using System.Collections.Generic;
using System.Linq;

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
            vertex[v] = null;

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
    }
}